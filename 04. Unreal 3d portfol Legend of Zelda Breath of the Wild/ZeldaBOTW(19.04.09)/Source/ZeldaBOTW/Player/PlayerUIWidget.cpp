// Fill out your copyright notice in the Description page of Project Settings.

#include "PlayerUIWidget.h"
#include "Object/ItemClass.h"
#include "Components/Image.h"
#include "Components/Button.h"
#include "Components/WrapBox.h"
#include "Components/Overlay.h"
#include "Components/TextBlock.h"
#include "Components/VerticalBox.h"
#include "Components/CanvasPanel.h"
#include "Player/PlayerInventory.h"
#include "Player/PlayerCharacter.h"
#include "Player/PlayerMesh.h"
#include "Player/Controller/PlayerCharacterController.h"
#include "Player/PlayerCharacterState.h"

UPlayerUIWidget::UPlayerUIWidget(const FObjectInitializer& ObjectInitializer):Super(ObjectInitializer)
{
	StaminaGageValue = 1.0f;
}

void UPlayerUIWidget::SetPlayerAim(bool value)
{
	AimOnOff = value;
	PlayerAimImage = Cast<UImage>(GetWidgetFromName(TEXT("PlayerAim")));
	if (IsValid(PlayerAimImage)) {
		if (value)PlayerAimImage->SetVisibility(ESlateVisibility::Visible);
		else PlayerAimImage->SetVisibility(ESlateVisibility::Hidden);
	}
}

void UPlayerUIWidget::SetArrowCount(int count)
{
	PlayerArrowCountText = Cast<UTextBlock>(GetWidgetFromName(TEXT("ArrowCount")));
	if (IsValid(PlayerArrowCountText)) {
		if (PlayerAimImage->Visibility == ESlateVisibility::Visible) {
			PlayerArrowCountText->SetText(FText::FromString("ArrowCount : " + FString::FromInt(count)));
			PlayerArrowCountText->SetVisibility(ESlateVisibility::Visible);
		}
		else {
			PlayerArrowCountText->SetVisibility(ESlateVisibility::Hidden);
		}
	}
}

void UPlayerUIWidget::SetHP(int maxHP, int curHP)
{
	if (curHP > maxHP)curHP = maxHP;
	HeartBox = Cast<UWrapBox>(GetWidgetFromName(TEXT("HP_BOX")));
	int heartCount = maxHP / 4;
	if (heartCount > HeartBox->GetChildrenCount()) {
		int value = heartCount - HeartBox->GetChildrenCount();
		for (int i = 0; i < value; i++) {
			UImage* hearImage2 = NewObject<UImage>();
			UTexture2D* Texture = getTexture(4);
			hearImage2->SetBrushFromTexture(Texture);
			HeartBox->AddChild(hearImage2);
			hearImage2->Brush.ImageSize = FVector2D(70.0f, 70.0f);
		}
	}
	for (int i = heartCount-1; i >= 0; i--) {
		int curHearImage = curHP - 4 * i;
		UTexture2D* Texture = getTexture(curHearImage);
		UImage* changeImage = Cast<UImage>(HeartBox->GetChildAt(i));
		if (IsValid(changeImage)) {
			changeImage->SetBrushFromTexture(Texture);
		}
	}
}

void UPlayerUIWidget::SetPontAlpha(UVerticalBox* MessageBox)
{
	MessageBox = Cast<UVerticalBox>(GetWidgetFromName(TEXT("MessageLog")));
	UTextBlock* Box_0 = Cast<UTextBlock>(MessageBox->GetChildAt(0));
	UTextBlock* Box_1 = Cast<UTextBlock>(MessageBox->GetChildAt(1));
	UTextBlock* Box_2 = Cast<UTextBlock>(MessageBox->GetChildAt(2));

	Box_0->SetColorAndOpacity(Box_1->ColorAndOpacity);
	Box_1->SetColorAndOpacity(Box_2->ColorAndOpacity);
	Box_2->SetColorAndOpacity(FSlateColor(FLinearColor(Box_2->ColorAndOpacity.GetSpecifiedColor().R, Box_2->ColorAndOpacity.GetSpecifiedColor().G, Box_2->ColorAndOpacity.GetSpecifiedColor().B, 1.0f)));

	GetWorld()->GetTimerManager().ClearTimer(Font1_TimerHandle);
	GetWorld()->GetTimerManager().SetTimer(Font1_TimerHandle, this, &UPlayerUIWidget::FontHide, 0.1f, true, 1.0f);
}

void UPlayerUIWidget::FontHide()
{
	MessageBox = Cast<UVerticalBox>(GetWidgetFromName(TEXT("MessageLog")));
	UTextBlock* Box_0 = Cast<UTextBlock>(MessageBox->GetChildAt(0));
	UTextBlock* Box_1 = Cast<UTextBlock>(MessageBox->GetChildAt(1));
	UTextBlock* Box_2 = Cast<UTextBlock>(MessageBox->GetChildAt(2));
	Box_0->SetColorAndOpacity(FSlateColor(FLinearColor(Box_0->ColorAndOpacity.GetSpecifiedColor().R, Box_0->ColorAndOpacity.GetSpecifiedColor().G, Box_0->ColorAndOpacity.GetSpecifiedColor().B, Box_0->ColorAndOpacity.GetSpecifiedColor().A - 0.025f)));
	Box_1->SetColorAndOpacity(FSlateColor(FLinearColor(Box_1->ColorAndOpacity.GetSpecifiedColor().R, Box_1->ColorAndOpacity.GetSpecifiedColor().G, Box_1->ColorAndOpacity.GetSpecifiedColor().B, Box_1->ColorAndOpacity.GetSpecifiedColor().A - 0.025f)));
	Box_2->SetColorAndOpacity(FSlateColor(FLinearColor(Box_2->ColorAndOpacity.GetSpecifiedColor().R, Box_2->ColorAndOpacity.GetSpecifiedColor().G, Box_2->ColorAndOpacity.GetSpecifiedColor().B, Box_2->ColorAndOpacity.GetSpecifiedColor().A - 0.025f)));
}

void UPlayerUIWidget::ShowInventory()
{
	auto InventoryUI = Cast<UCanvasPanel>(GetWidgetFromName(TEXT("InventoryUI")));
	auto IdleUI = Cast<UCanvasPanel>(GetWidgetFromName(TEXT("IdleUI")));

	auto PlayerCaracter = PlayerCharacterController->getPlayerCharacter();
	auto PlayerInventory = PlayerCaracter->getPlayerInvenroty();

	if (IsValid(InventoryUI) && IsValid(IdleUI)) {
		if (InventoryUI->Visibility == ESlateVisibility::Visible) {
			InventoryUI->SetVisibility(ESlateVisibility::Hidden);
			IdleUI->SetVisibility(ESlateVisibility::Visible);
			InventoryActive = false;
			PlayerCharacterController->bShowMouseCursor = false;
			PlayerCharacterController->bEnableClickEvents = false;
			PlayerCharacterController->bEnableMouseOverEvents = false;
		}
		else {
			InventoryUI->SetVisibility(ESlateVisibility::Visible);
			IdleUI->SetVisibility(ESlateVisibility::Hidden);
			InventoryActive = true;
			PlayerCharacterController->bShowMouseCursor = true;
			PlayerCharacterController->bEnableClickEvents = true;
			PlayerCharacterController->bEnableMouseOverEvents = true;
			changeItemType(ItemTypeValue::ItemType_Weapon, 0);

			auto InventoryUI = Cast<UTextBlock>(GetWidgetFromName(TEXT("MoneyCountText")));
			FString name =  FString::FromInt(PlayerInventory->getPlayerMoneyCount());
			InventoryUI->SetText(FText::FromString(name));
		}
	}
	ResetInformation();
}

void UPlayerUIWidget::PrintMessageBox(FString Message)
{
	MessageBox = Cast<UVerticalBox>(GetWidgetFromName(TEXT("MessageLog")));
	for (int i = 0; i < 3; i++) {
		int index = i + 1;
		UTextBlock* PlayerArrowCountText = Cast<UTextBlock>(MessageBox->GetChildAt(i));
		UTextBlock* pre_PlayerArrowCountText = Cast<UTextBlock>(MessageBox->GetChildAt(index));
		if (index == 3) {
			PlayerArrowCountText->SetText(FText::FromString(Message));
		}
		else {
			PlayerArrowCountText->SetText(pre_PlayerArrowCountText->GetText());
		}
	}
	SetPontAlpha(MessageBox);
}

void UPlayerUIWidget::setInventoryButtonImage()
{
	auto PlayerCaracter = PlayerCharacterController->getPlayerCharacter();
	auto PlayerInventory = PlayerCaracter->getPlayerInvenroty();

	TArray<UTexture2D*> imageList;
	for (int i = 0; i < PlayerInventory->getWeapon_SwordList().Num(); i++) {
		imageList.Add(PlayerInventory->getWeapon_SwordList()[i]->getItemImage());
	}
	setInventoryImage(imageList);
}

void UPlayerUIWidget::setInventoryImage(TArray<UTexture2D*> ImageList)
{
	for (int i = 0; i < 20; i++) {
		FString name = "InventoryImage_" + FString::FromInt(i);
		const TCHAR* chars = *name;
		auto InventoryUI = Cast<UImage>(GetWidgetFromName(FName(chars)));
		InventoryUI->SetColorAndOpacity(FLinearColor(0, 0, 0,0));
		InventoryUI->SetBrushFromTexture(nullptr);
	}
	for (int i = 0; i < ImageList.Num(); i++) {
		FString name = "InventoryImage_"+FString::FromInt(i);
		const TCHAR* chars = *name;
		auto InventoryUI = Cast<UImage>(GetWidgetFromName(FName(chars)));
		if (IsValid(InventoryUI) && ImageList[i]!=nullptr) {
			InventoryUI->SetColorAndOpacity(FLinearColor(1, 1,1, 1));
			InventoryUI->SetBrushFromTexture(ImageList[i]);
		}
	}
}

void UPlayerUIWidget::ShowStamina(bool value)
{
	auto StaminaGage = Cast<UImage>(GetWidgetFromName(TEXT("StanminaGage")));
	if (value) {
		StaminaGage->SetVisibility(ESlateVisibility::Visible);
	}
	else {
		StaminaGage->SetVisibility(ESlateVisibility::Hidden);
	}
}

void UPlayerUIWidget::ShowShopButton(bool value)
{
	auto BackShopButton = Cast<UButton>(GetWidgetFromName(TEXT("BackShop")));
	auto SellButton = Cast<UButton>(GetWidgetFromName(TEXT("SellButton")));
	auto PriceText = Cast<UOverlay>(GetWidgetFromName(TEXT("PriceMenu")));
	ResetInformation();
	if (value) {
		BackShopButton->SetVisibility(ESlateVisibility::Visible);
		SellButton->SetVisibility(ESlateVisibility::Visible);
		PriceText->SetVisibility(ESlateVisibility::Visible);
		ShopUI = true;
	}
	else {
		BackShopButton->SetVisibility(ESlateVisibility::Hidden);
		SellButton->SetVisibility(ESlateVisibility::Hidden);
		PriceText->SetVisibility(ESlateVisibility::Hidden);
		ShopUI = false;
	}
}

void UPlayerUIWidget::HiddenShopButton()
{
	auto PlayerController = GetWorld()->GetFirstPlayerController();
	auto PController = Cast< APlayerCharacterController>(PlayerController);

	ShowShopButton(false);
	PController->getPlayerCharacter()->getPlayerUI()->ShowInventory();
}

void UPlayerUIWidget::changeItemType(ItemTypeValue typeValue, int num)
{
	auto PlayerCaracter = PlayerCharacterController->getPlayerCharacter();
	auto PlayerInventory = PlayerCaracter->getPlayerInvenroty();
	TArray<UTexture2D*> imageList;

	FString name;
	const TCHAR* chars = *name;
	for (int i = 0; i < 6; i++) {
		name = "MenuIconImage_" + FString::FromInt(i);
		chars = *name;
		auto MenuUI = Cast<UImage>(GetWidgetFromName(FName(chars)));
		if (i != num) {
			MenuUI->SetColorAndOpacity(FLinearColor(0, 0, 0, 0.4));
		}
		else {
			MenuUI->SetColorAndOpacity(FLinearColor(0, 0, 0, 1.0));
		}
	}
	curInventoryItemType = typeValue;
	switch (typeValue) {
		case ItemTypeValue::ItemType_Weapon:
			for (int i = 0; i < PlayerInventory->getWeapon_SwordList().Num(); i++) {
				imageList.Add(PlayerInventory->getWeapon_SwordList()[i]->getItemImage());
			}
			break;
		case ItemTypeValue::ItemType_Bow:
			for (int i = 0; i < PlayerInventory->getWeapon_BowList().Num(); i++) {
				imageList.Add(PlayerInventory->getWeapon_BowList()[i]->getItemImage());
			}
			break;
		case ItemTypeValue::ItemType_Shield:
			for (int i = 0; i < PlayerInventory->getWeapon_ShieldList().Num(); i++) {
				imageList.Add(PlayerInventory->getWeapon_ShieldList()[i]->getItemImage());
			}
			break;
		case ItemTypeValue::ItemType_Tshirts:
			for (int i = 0; i < PlayerInventory->getItem_ShirtsList().Num(); i++) {
				imageList.Add(PlayerInventory->getItem_ShirtsList()[i]->getItemImage());
			}
			break;
		case ItemTypeValue::ItemType_Pants:
			for (int i = 0; i < PlayerInventory->getItem_PantsList().Num(); i++) {
				imageList.Add(PlayerInventory->getItem_PantsList()[i]->getItemImage());
			}
			break;
	}
	setInventoryImage(imageList);
}

void UPlayerUIWidget::IventoryButton(UImage * image)
{
	UTexture2D* tex2 =  Cast<UTexture2D>(image->Brush.GetResourceObject());
	if (IsValid(tex2)) {
		auto PlayerController = GetWorld()->GetFirstPlayerController();
		auto PController = Cast< APlayerCharacterController>(PlayerController);
		auto PlayerCaracter = PController->getPlayerCharacter();
		auto PlayerInventory = PlayerCaracter->getPlayerInvenroty();

		FName itemName;
		FString iteminfo;
		switch (curInventoryItemType) {
		case ItemTypeValue::ItemType_Weapon:
			for (int i = 0; i < PlayerInventory->getWeapon_SwordList().Num(); i++) {
				if (tex2 == PlayerInventory->getWeapon_SwordList()[i]->getItemImage()) {
					itemName = (PlayerInventory->getWeapon_SwordList()[i])->getItemName();
					iteminfo = PlayerInventory->getWeapon_SwordList()[i]->getItemInformation();
					PController->getPlayerWeapon()->ChangeWeaponInventory(PlayerInventory->getWeapon_SwordList()[i]);
					
					PlayerInventory->setCurItemName(itemName);
					PlayerInventory->setCurItemImage(PlayerInventory->getWeapon_SwordList()[i]->getItemImage());
					PlayerInventory->setCurItemInformation(iteminfo);
					PlayerInventory->setCurItemType(ItemTypeValue::ItemType_Weapon);
					PlayerInventory->setCurItemPrice(PlayerInventory->getWeapon_SwordList()[i]->getItemPrice());
					
					auto PriceText = Cast<UTextBlock>(GetWidgetFromName(TEXT("Price")));
					FString name = "Price:"+FString::FromInt(PlayerInventory->getWeapon_SwordList()[i]->getItemPrice());
					PriceText->SetText(FText::FromString(name));
					break;
				}
			}
			break;
		case ItemTypeValue::ItemType_Bow:
			for (int i = 0; i < PlayerInventory->getWeapon_BowList().Num(); i++) {
				if (tex2 == PlayerInventory->getWeapon_BowList()[i]->getItemImage()) {
					itemName = (PlayerInventory->getWeapon_BowList()[i])->getItemName();
					iteminfo = PlayerInventory->getWeapon_BowList()[i]->getItemInformation();
					PController->getPlayerWeapon()->ChangeWeaponInventory(PlayerInventory->getWeapon_BowList()[i]);
					
					PlayerInventory->setCurItemName(itemName);
					PlayerInventory->setCurItemImage(PlayerInventory->getWeapon_BowList()[i]->getItemImage());
					PlayerInventory->setCurItemInformation(iteminfo);
					PlayerInventory->setCurItemType(ItemTypeValue::ItemType_Bow);
					PlayerInventory->setCurItemPrice(PlayerInventory->getWeapon_BowList()[i]->getItemPrice());
					
					auto PriceText = Cast<UTextBlock>(GetWidgetFromName(TEXT("Price")));
					FString name = "Price:" + FString::FromInt(PlayerInventory->getWeapon_BowList()[i]->getItemPrice());
					PriceText->SetText(FText::FromString(name));
					break;
				}
			}
			break;
		case ItemTypeValue::ItemType_Shield:
			for (int i = 0; i < PlayerInventory->getWeapon_ShieldList().Num(); i++) {
				if (tex2 == PlayerInventory->getWeapon_ShieldList()[i]->getItemImage()) {
					itemName = (PlayerInventory->getWeapon_ShieldList()[i])->getItemName();
					iteminfo = PlayerInventory->getWeapon_ShieldList()[i]->getItemInformation();
					PController->getPlayerWeapon()->ChangeWeaponInventory(PlayerInventory->getWeapon_ShieldList()[i]);

					PlayerInventory->setCurItemName(itemName);
					PlayerInventory->setCurItemImage(PlayerInventory->getWeapon_ShieldList()[i]->getItemImage());
					PlayerInventory->setCurItemInformation(iteminfo);
					PlayerInventory->setCurItemType(ItemTypeValue::ItemType_Shield);
					PlayerInventory->setCurItemPrice(PlayerInventory->getWeapon_ShieldList()[i]->getItemPrice());
					
					auto PriceText = Cast<UTextBlock>(GetWidgetFromName(TEXT("Price")));
					FString name = "Price:" + FString::FromInt(PlayerInventory->getWeapon_ShieldList()[i]->getItemPrice());
					PriceText->SetText(FText::FromString(name));
					break;
				}
			}
			break;
		case ItemTypeValue::ItemType_Tshirts:
			for (int i = 0; i < PlayerInventory->getItem_ShirtsList().Num(); i++) {
				if (tex2 == PlayerInventory->getItem_ShirtsList()[i]->getItemImage()) {
					itemName = (PlayerInventory->getItem_ShirtsList()[i])->getItemName();
					iteminfo = PlayerInventory->getItem_ShirtsList()[i]->getItemInformation();

					PlayerInventory->setCurItemName(itemName);
					PlayerInventory->setCurItemImage(PlayerInventory->getItem_ShirtsList()[i]->getItemImage());
					PlayerInventory->setCurItemInformation(iteminfo);
					PlayerInventory->setCurItemType(ItemTypeValue::ItemType_Tshirts);
					PlayerInventory->setCurItemPrice(PlayerInventory->getItem_ShirtsList()[i]->getItemPrice());
					
					PlayerCaracter->getPlayerMeshComponent()->SetUpperMesh(PlayerInventory->getItem_ShirtsList()[i]->getClothesMesh());
					PController->getPlayerState()->setPlayerState(E_PlayerState::Player_SWORD);
					
					auto PriceText = Cast<UTextBlock>(GetWidgetFromName(TEXT("Price")));
					FString name = "Price:" + FString::FromInt(PlayerInventory->getItem_ShirtsList()[i]->getItemPrice());
					PriceText->SetText(FText::FromString(name));
					break;
				}
			}
			break;
		case ItemTypeValue::ItemType_Pants:
			for (int i = 0; i < PlayerInventory->getItem_PantsList().Num(); i++) {
				if (tex2 == PlayerInventory->getItem_PantsList()[i]->getItemImage()) {
					itemName = (PlayerInventory->getItem_PantsList()[i])->getItemName();
					iteminfo = PlayerInventory->getItem_PantsList()[i]->getItemInformation();

					PlayerInventory->setCurItemName(itemName);
					PlayerInventory->setCurItemImage(PlayerInventory->getItem_PantsList()[i]->getItemImage());
					PlayerInventory->setCurItemInformation(iteminfo);
					PlayerInventory->setCurItemType(ItemTypeValue::ItemType_Pants);
					PlayerInventory->setCurItemPrice(PlayerInventory->getItem_PantsList()[i]->getItemPrice());
					
					PlayerCaracter->getPlayerMeshComponent()->SetUnderMesh(PlayerInventory->getItem_PantsList()[i]->getClothesMesh());
					PController->getPlayerState()->setPlayerState(E_PlayerState::Player_SWORD);
					
					auto PriceText = Cast<UTextBlock>(GetWidgetFromName(TEXT("Price")));
					FString name = "Price:" + FString::FromInt(PlayerInventory->getItem_PantsList()[i]->getItemPrice());
					PriceText->SetText(FText::FromString(name));
					break;
				}
			}
			break;
		}
		PController->getPlayerWeapon()->setWeaponComponent(E_Weapon::Weapon_None, PController->getPlayerCharacter()->getPlayerMeshComponent()->getRootMesh(), TEXT(""));
		auto NameText = Cast<UTextBlock>(GetWidgetFromName(TEXT("ItemNameText")));
		InfomationText = Cast<UTextBlock>(GetWidgetFromName(TEXT("ItemInformationText")));
		if (IsValid(NameText) ) {
			NameText->SetText(FText::FromName(itemName));
			InfomationText->SetText(FText::FromString(iteminfo));
		}
	}
}

void UPlayerUIWidget::sellItemButton()
{
	auto PlayerController = GetWorld()->GetFirstPlayerController();
	auto PController = Cast< APlayerCharacterController>(PlayerController);
	auto PlayerCaracter = PController->getPlayerCharacter();
	auto PlayerInventory = PlayerCaracter->getPlayerInvenroty();

	if (PlayerInventory->getCurItemName() != "") {
		switch (PlayerInventory->getCurItemType()) {
		case ItemTypeValue::ItemType_Weapon:
			for (int i = 0; i < PlayerInventory->getWeapon_SwordList().Num(); i++) {
				if (PlayerInventory->getCurItemImage() == PlayerInventory->getWeapon_SwordList()[i]->getItemImage()) {
					int moneyValue = 0;
					moneyValue = PlayerInventory->getPlayerMoneyCount();
					moneyValue += PlayerInventory->getWeapon_SwordList()[i]->getItemPrice();
					PController->getPlayerWeapon()->getPlayerSworld()->deleteWeaponMesh();
					PlayerInventory->setPlayerMoneyCount(moneyValue);
					PlayerInventory->DeleteWaepon_SwordList(PlayerInventory->getWeapon_SwordList()[i]);
					changeItemType(ItemTypeValue::ItemType_Weapon, 0);
					ResetInformation();
					PController->getPlayerWeapon()->setWeaponPlayerCharacter(E_Weapon::Weapon_SWORD);
					break;
				}
			}
			break;
		case ItemTypeValue::ItemType_Bow:
			for (int i = 0; i < PlayerInventory->getWeapon_BowList().Num(); i++) {
				if (PlayerInventory->getCurItemImage() == PlayerInventory->getWeapon_BowList()[i]->getItemImage()) {
					int moneyValue = 0;
					moneyValue = PlayerInventory->getPlayerMoneyCount();
					moneyValue += PlayerInventory->getWeapon_BowList()[i]->getItemPrice();
					
					PController->getPlayerWeapon()->getPlayerBow()->deleteWeaponMesh();
					PlayerInventory->setPlayerMoneyCount(moneyValue);
					PlayerInventory->DeleteWaepon_BowList(PlayerInventory->getWeapon_BowList()[i]);
					changeItemType(ItemTypeValue::ItemType_Bow, 1);
					ResetInformation();
					PController->getPlayerWeapon()->setWeaponPlayerCharacter(E_Weapon::Weapon_BOW);

					break;
				}
			}
			break;
		case ItemTypeValue::ItemType_Shield:
			for (int i = 0; i < PlayerInventory->getWeapon_ShieldList().Num(); i++) {
				if (PlayerInventory->getCurItemImage() == PlayerInventory->getWeapon_ShieldList()[i]->getItemImage()) {
					int moneyValue = 0;
					moneyValue = PlayerInventory->getPlayerMoneyCount();
					moneyValue += PlayerInventory->getWeapon_ShieldList()[i]->getItemPrice();
				
					PController->getPlayerWeapon()->getPlayerShield()->deleteWeaponMesh();
					PlayerInventory->setPlayerMoneyCount(moneyValue);
					PlayerInventory->DeleteWaepon_ShieldList(PlayerInventory->getWeapon_ShieldList()[i]);
					changeItemType(ItemTypeValue::ItemType_Shield, 2);
					ResetInformation(); 
					PController->getPlayerWeapon()->setWeaponPlayerCharacter(E_Weapon::Weapon_SHIELD);

					break;
				}
			}
			break;
		case ItemTypeValue::ItemType_Tshirts:
			for (int i = 0; i < PlayerInventory->getItem_ShirtsList().Num(); i++) {
				if (PlayerInventory->getCurItemImage() == PlayerInventory->getItem_ShirtsList()[i]->getItemImage()) {
					int moneyValue = 0;
					moneyValue = PlayerInventory->getPlayerMoneyCount();
					moneyValue += PlayerInventory->getItem_ShirtsList()[i]->getItemPrice();

					PlayerCaracter->getPlayerMeshComponent()->getUpperMesh()->SetVisibility(false);

					PlayerInventory->setPlayerMoneyCount(moneyValue);
					PlayerInventory->DeleteWaepon_ShirtsList(PlayerInventory->getItem_ShirtsList()[i]);

					int ShirtsListSize = PlayerInventory->getItem_ShirtsList().Num();
					if (ShirtsListSize == 0) {
						PlayerCaracter->getPlayerMeshComponent()->SetBaseUpperMesh();
					}
					else {
						PlayerCaracter->getPlayerMeshComponent()->SetUpperMesh(PlayerInventory->getItem_ShirtsList()[ShirtsListSize - 1]->getClothesMesh());
					}
					PlayerCaracter->getPlayerMeshComponent()->getUpperMesh()->SetVisibility(true);

					changeItemType(ItemTypeValue::ItemType_Tshirts,3);
					ResetInformation();
					break;
				}
			}
			break;
		case ItemTypeValue::ItemType_Pants:
			for (int i = 0; i < PlayerInventory->getItem_PantsList().Num(); i++) {
				if (PlayerInventory->getCurItemImage() == PlayerInventory->getItem_PantsList()[i]->getItemImage()) {
					int moneyValue = 0;
					moneyValue = PlayerInventory->getPlayerMoneyCount();
					moneyValue += PlayerInventory->getItem_PantsList()[i]->getItemPrice();
					
					PlayerCaracter->getPlayerMeshComponent()->getUnderMesh()->SetVisibility(false);

					PlayerInventory->setPlayerMoneyCount(moneyValue);
					PlayerInventory->DeleteWaepon_PantsList(PlayerInventory->getItem_PantsList()[i]);
					
					int PantsListSize = PlayerInventory->getItem_PantsList().Num();
					if (PantsListSize == 0) {
						PlayerCaracter->getPlayerMeshComponent()->SetBaseUnderMesh();
					}
					else {
						PlayerCaracter->getPlayerMeshComponent()->SetUnderMesh(PlayerInventory->getItem_PantsList()[PantsListSize - 1]->getClothesMesh());
					}
					PlayerCaracter->getPlayerMeshComponent()->getUnderMesh()->SetVisibility(true);

					changeItemType(ItemTypeValue::ItemType_Pants, 4);
					ResetInformation();
					break;
				}
			}
			break;
		}

	}
	auto InventoryUI = Cast<UTextBlock>(GetWidgetFromName(TEXT("MoneyCountText")));
	FString name = FString::FromInt(PlayerInventory->getPlayerMoneyCount());
	InventoryUI->SetText(FText::FromString(name));
}

void UPlayerUIWidget::ResetInformation()
{
	auto NameText = Cast<UTextBlock>(GetWidgetFromName(TEXT("ItemNameText")));
	InfomationText = Cast<UTextBlock>(GetWidgetFromName(TEXT("ItemInformationText")));
	if (IsValid(NameText)) {
		NameText->SetText(FText::FromName(""));
		InfomationText->SetText(FText::FromString(""));
	}
	auto PriceText = Cast<UTextBlock>(GetWidgetFromName(TEXT("Price")));
	FString name = "Price:" + FString::FromInt(0);
	PriceText->SetText(FText::FromString(name));
}

UTexture2D * UPlayerUIWidget::getTexture(int num)
{
	FString Path;
	if (num < 0)num = 0;
	else if (num > 4)num = 4;
	Path = FString("/Game/UI/UI_IMAGE/Heart/Heart"+FString::FromInt(num)+".Heart" + FString::FromInt(num));
	UTexture2D* returnTexture = Cast<UTexture2D>(StaticLoadObject(UTexture2D::StaticClass(), NULL, *Path));

	return returnTexture;
}