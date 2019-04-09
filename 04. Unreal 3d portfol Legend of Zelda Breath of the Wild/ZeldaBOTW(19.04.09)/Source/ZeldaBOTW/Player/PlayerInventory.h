// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ZeldaBOTW.h"
#include "Object/TotalItemClass.h"
#include "Weapon/Weapon_Arrow.h"
#include "Weapon/Weapon_Equip.h"
#include "Object/ItemClass.h"
#include "Object/ItemClassFood.h"
#include "Object/ItemClassMaterial.h"
#include "Object/ItemClassClothes.h"
#include "Components/ActorComponent.h"
#include "PlayerInventory.generated.h"


UCLASS( ClassGroup=(Custom), meta=(BlueprintSpawnableComponent) )
class ZELDABOTW_API UPlayerInventory : public UActorComponent
{
	GENERATED_BODY()
private:
	TArray<AWeapon_Equip*> Weapon_BowList;
	TArray<AWeapon_Equip*> Weapon_ShieldList;
	TArray<AWeapon_Equip*> Weapon_SwordList;
	TArray<AItemClassFood*> Item_FoodList;
	TArray<AItemClassMaterial*> Item_MaterialList;
	TArray<AItemClassClothes*> Item_ShirtsList;
	TArray<AItemClassClothes*> Item_PantsList;

	UPROPERTY(Category = Inventory, EditAnywhere, BlueprintReadWrite, meta = (AllowPrivateAccess = "true"))
		int32 PlayerMoney;
	UPROPERTY(Category = Inventory, EditAnywhere, BlueprintReadWrite, meta = (AllowPrivateAccess = "true"))
		FName			curItemName;
	UPROPERTY(Category = Inventory, EditAnywhere, BlueprintReadWrite, meta = (AllowPrivateAccess = "true"))
		UTexture2D*		curItemImage;
	UPROPERTY(Category = Inventory, EditAnywhere, BlueprintReadWrite, meta = (AllowPrivateAccess = "true"))
		FString			curItemInformation;
	UPROPERTY(Category = Inventory, EditAnywhere, BlueprintReadWrite, meta = (AllowPrivateAccess = "true"))
		ItemTypeValue	curItemType;
	UPROPERTY(Category = Inventory, EditAnywhere, BlueprintReadWrite, meta = (AllowPrivateAccess = "true"))
		float			curItemPrice;

public:	
	UPlayerInventory();

public:	
	FName getCurItemName() { return curItemName; }
	UTexture2D* getCurItemImage() { return curItemImage; }
	FString getCurItemInformation() { return curItemInformation; }
	ItemTypeValue getCurItemType() { return curItemType; }
	float getCurItemPrice() { return curItemPrice; }

	void AddSwordInventory(AWeapon_Equip* sword);
	void AddBowdInventory(AWeapon_Equip* Bow);
	void AddShieldInventory(AWeapon_Equip* Shield);
	void AddFoodInventory(AItemClassFood* Food);
	void AddMaterialInventory(AItemClassMaterial* Material);
	void AddShirtsInventory(AItemClassClothes* Shirts);
	void AddPantsInventory(AItemClassClothes* Pants);

	void DeleteWaepon_SwordList(AWeapon_Equip* weapon);
	void DeleteWaepon_BowList(AWeapon_Equip* weapon);
	void DeleteWaepon_ShieldList(AWeapon_Equip* weapon);
	void DeleteWaepon_ShirtsList(AItemClassClothes* weapon);
	void DeleteWaepon_PantsList(AItemClassClothes* weapon);

	void setPlayerMoneyCount(int value) { PlayerMoney = value; }

	void setCurItemName(FName itemName) { curItemName = itemName; }
	void setCurItemImage(UTexture2D* ItemTexture) { curItemImage = ItemTexture; }
	void setCurItemInformation(FString ItemInformation) { curItemInformation = ItemInformation; }
	void setCurItemType(ItemTypeValue itemType) { curItemType = itemType;}
	void setCurItemPrice(float itemPrice) { curItemPrice = itemPrice; }

	TArray<AWeapon_Equip*> getWeapon_BowList() { return Weapon_BowList; }
	TArray<AWeapon_Equip*> getWeapon_SwordList() { return Weapon_SwordList; }
	TArray<AWeapon_Equip*> getWeapon_ShieldList() { return Weapon_ShieldList; }
	TArray<AItemClassFood*> getItem_FoodList() { return Item_FoodList; }
	TArray<AItemClassMaterial*> getItem_MaterialList() { return Item_MaterialList; }
	TArray<AItemClassClothes*> getItem_ShirtsList() { return Item_ShirtsList; }
	TArray<AItemClassClothes*> getItem_PantsList() { return Item_PantsList; }

	int32 getPlayerMoneyCount() { return PlayerMoney; }
};
