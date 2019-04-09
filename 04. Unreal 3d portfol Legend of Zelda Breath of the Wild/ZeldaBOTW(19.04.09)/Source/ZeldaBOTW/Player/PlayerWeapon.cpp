// Fill out your copyright notice in the Description page of Project Settings.

#include "PlayerWeapon.h"
#include "Player/PlayerCharacter.h"
#include "Player/Controller/PlayerCharacterController.h"
#include "Player/PlayerUIWidget.h"
#include "Player/PlayerInventory.h"

void APlayerWeapon::BeginPlay()
{
	Super::BeginPlay();
	PlayerChracterController = Cast< APlayerCharacterController>(GetWorld()->GetFirstPlayerController());
}

APlayerWeapon::APlayerWeapon()
{
 	PrimaryActorTick.bCanEverTick = false;
	Show_Arrow = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("Show_Arrow"));
	Weapon_Sword = CreateDefaultSubobject<AWeapon_Equip>(TEXT("Weapon_Sword"));
	Weapon_Shield = CreateDefaultSubobject<AWeapon_Equip>(TEXT("Weapon_Shield"));
	Weapon_Bow = CreateDefaultSubobject<AWeapon_Equip>(TEXT("Weapon_Bow"));
	ArrowBag = CreateDefaultSubobject<AWeapon_Equip>(TEXT("ArrowBag"));

	static ConstructorHelpers::FObjectFinder<UStaticMesh>SK_Arrow(TEXT("/Game/OBJECT/Bow/Arrow/WeaponArrow.WeaponArrow"));
	if (SK_Arrow.Succeeded()) {
		Show_Arrow->SetStaticMesh(SK_Arrow.Object);
	}

	Show_Arrow->SetCollisionProfileName(TEXT("NoCollision"));
	
	Weapon_Arrows = AWeapon_Arrow::StaticClass();
	ArrowCount = 6;
	WeaponSwordNum = -1;
	WeaponShieldNum = -1;
	WeaponBowNum = -1;
}

void APlayerWeapon::setWeaponComponent(E_Weapon Weapon, USkeletalMeshComponent * MeshComponent, const TCHAR * Socket)
{
	switch (Weapon) {
	case E_Weapon::Weapon_BOW:
		//if (IsValid(Weapon_Bow)) {
			Weapon_Bow->getWeaponMesh()->AttachToComponent(MeshComponent, FAttachmentTransformRules::SnapToTargetIncludingScale, Socket);
			Weapon_Bow->getWeaponMesh()->SetVisibility(true);
			Weapon_Bow->getWeaponMesh()->SetCollisionProfileName(TEXT("Weapon"));
		//}
		//if (IsValid(Weapon_Sword)) {
			Weapon_Sword->getWeaponMesh()->AttachToComponent(MeshComponent, FAttachmentTransformRules::KeepWorldTransform, TEXT("LeftUpLegSocket"));
			Weapon_Sword->getWeaponMesh()->SetRelativeRotation(FRotator(0.0f, 0.0f, 0.0f));
			Weapon_Sword->getWeaponMesh()->SetRelativeLocation(FVector(0.0f, 0.0f, -10.0f));
			Weapon_Sword->getWeaponMesh()->SetCollisionProfileName(TEXT("NoCollision"));
		//}
		//if (IsValid(Weapon_Shield)) {
			Weapon_Shield->getWeaponMesh()->AttachToComponent(MeshComponent, FAttachmentTransformRules::SnapToTargetIncludingScale, TEXT("PlayerBackStorage"));
			Weapon_Shield->getWeaponMesh()->SetRelativeRotation(FRotator(180.0f, 180.0f, 0.0f));
			Weapon_Shield->getWeaponMesh()->SetRelativeLocation(FVector(20.0f, -2.0f, 0.0f));
			Weapon_Shield->getWeaponMesh()->SetCollisionProfileName(TEXT("NoCollision"));
		//}
		break;
	case E_Weapon::Weapon_SWORD:
		//if (IsValid(Weapon_Bow)) {
			Weapon_Bow->getWeaponMesh()->AttachToComponent(MeshComponent, FAttachmentTransformRules::SnapToTargetIncludingScale, TEXT("PlayerBackStorage"));
			Weapon_Bow->getWeaponMesh()->SetCollisionProfileName(TEXT("NoCollision"));
		//}
		//if (IsValid(Weapon_Sword)) {
			Weapon_Sword->getWeaponMesh()->AttachToComponent(MeshComponent, FAttachmentTransformRules::KeepWorldTransform, Socket);
			Weapon_Sword->getWeaponMesh()->SetRelativeRotation(FRotator(180.0f, 0.0f, -60.0f));
			Weapon_Sword->getWeaponMesh()->SetRelativeLocation(FVector(0.0f, 0.0f, 0.0f));
			Weapon_Sword->getWeaponMesh()->SetVisibility(true);
			Weapon_Sword->getWeaponMesh()->SetCollisionProfileName(TEXT("Weapon"));
		//}
		break;
	case E_Weapon::Weapon_SHIELD:
		//if (IsValid(Weapon_Shield)) {
			Weapon_Shield->getWeaponMesh()->AttachToComponent(MeshComponent, FAttachmentTransformRules::SnapToTargetIncludingScale, Socket);
			Weapon_Shield->getWeaponMesh()->SetRelativeRotation(FRotator(140.0f, 120.0f, 80.0f));
			Weapon_Shield->getWeaponMesh()->SetRelativeLocation(FVector(10.0f, -10.0f, 10.0f));
			Weapon_Shield->getWeaponMesh()->SetVisibility(true);
			Weapon_Shield->getWeaponMesh()->SetCollisionProfileName(TEXT("Weapon"));
		//}
		break;
	case E_Weapon::Weapon_ARROW_BAG:
		//if (IsValid(ArrowBag)) {
			ArrowBag->getWeaponMesh()->AttachToComponent(MeshComponent, FAttachmentTransformRules::SnapToTargetIncludingScale, Socket);
			ArrowBag->getWeaponMesh()->SetRelativeLocation(FVector(0.0f, -2.0f, 0.0f));
		//}
		break;
	case E_Weapon::Weapon_None:
		if (IsValid(Weapon_Sword)) {
			Weapon_Sword->getWeaponMesh()->AttachToComponent(MeshComponent, FAttachmentTransformRules::KeepWorldTransform, TEXT("LeftUpLegSocket"));
			Weapon_Sword->getWeaponMesh()->SetRelativeRotation(FRotator(0.0f, 0.0f, 0.0f));
			Weapon_Sword->getWeaponMesh()->SetRelativeLocation(FVector(0.0f, 0.0f, -10.0f));
			Weapon_Sword->getWeaponMesh()->SetVisibility(true);
			Weapon_Sword->getWeaponMesh()->SetCollisionProfileName(TEXT("NoCollision"));
		}	
		if (IsValid(Weapon_Shield)) {
			Weapon_Shield->getWeaponMesh()->AttachToComponent(MeshComponent, FAttachmentTransformRules::SnapToTargetIncludingScale, TEXT("PlayerBackStorage"));
			Weapon_Shield->getWeaponMesh()->SetRelativeRotation(FRotator(180.0f, 180.0f, 0.0f));
			Weapon_Shield->getWeaponMesh()->SetRelativeLocation(FVector(20.0f, -2.0f, 0.0f));
			Weapon_Shield->getWeaponMesh()->SetVisibility(true);
			Weapon_Shield->getWeaponMesh()->SetCollisionProfileName(TEXT("NoCollision"));
		}
		if (IsValid(Weapon_Bow)) {
			Weapon_Bow->getWeaponMesh()->AttachToComponent(MeshComponent, FAttachmentTransformRules::SnapToTargetIncludingScale, TEXT("PlayerBackStorage"));
			Weapon_Bow->getWeaponMesh()->SetVisibility(true);
			Weapon_Bow->getWeaponMesh()->SetCollisionProfileName(TEXT("NoCollision"));
		}
		if (IsValid(ArrowBag)) {
			ArrowBag->getWeaponMesh()->AttachToComponent(MeshComponent, FAttachmentTransformRules::SnapToTargetIncludingScale, TEXT("BackWaistSocket"));
			ArrowBag->getWeaponMesh()->SetRelativeLocation(FVector(0.0f, -2.0f, 0.0f));
		}
		break;
	}
}

void APlayerWeapon::FireArrow(FVector location, FRotator Rotation, FVector Forward)
{
	auto PlayerUI = PlayerChracterController->getPlayerCharacter()->getPlayerUI();
	PlayerUI->SetArrowCount(ArrowCount);
	Show_Arrow->SetVisibility(false);
	if (ArrowCount > 0) {
		ArrowCount--;

		auto Arrow = GetWorld()->SpawnActor<AWeapon_Arrow>(Weapon_Arrows, location, Rotation);
		FVector NewVelocity = Forward * 1000.0f;
		Arrow->setVelocity(NewVelocity);
		Arrow->setHitEnemy(true);
	}
}

void APlayerWeapon::ShowArrowLocationRotation(FVector Location, FRotator Rotation, bool aimOnOFf)
{
	auto PlayerUI = PlayerChracterController->getPlayerCharacter()->getPlayerUI();
	PlayerUI->SetArrowCount(ArrowCount);
	if (aimOnOFf && ArrowCount > 0)
	{
		Show_Arrow->SetVisibility(aimOnOFf);
		Show_Arrow->SetRelativeLocation(Location);
		Show_Arrow->SetRelativeRotation(Rotation + FRotator(0.0f, -90.0f, 0.0f));
	}
}

void APlayerWeapon::ChangeWeapon(E_Weapon weaponType, bool Upvalue)
{
	auto PlayerInvenroty = PlayerChracterController->getPlayerCharacter()->getPlayerInvenroty();
	switch (weaponType) {
		case E_Weapon::Weapon_SWORD:
			Weapon_Sword->getWeaponMesh()->SetVisibility(false);
			if (Upvalue) {
				if (PlayerInvenroty->getWeapon_SwordList().Num() - 1 > WeaponSwordNum) {
					WeaponSwordNum++;
				}
				else {
					WeaponSwordNum = 0;
				}
			}
			else {
				if (PlayerInvenroty->getWeapon_SwordList().Num() > 1) {
					if (WeaponSwordNum == 0) {
						WeaponSwordNum = PlayerInvenroty->getWeapon_SwordList().Num() - 1;
					}
					else {
						WeaponSwordNum--;
					}
				}
			}
			Weapon_Sword = PlayerInvenroty->getWeapon_SwordList()[WeaponSwordNum % PlayerInvenroty->getWeapon_SwordList().Num()];
			Weapon_Sword->getWeaponMesh()->SetCollisionProfileName(TEXT("PlayerWeapon"));
			Weapon_Sword->getWeaponMesh()->SetVisibility(true);
			break;
		case E_Weapon::Weapon_BOW:
			Weapon_Bow->getWeaponMesh()->SetVisibility(false);
			if (Upvalue) {
				if (PlayerInvenroty->getWeapon_BowList().Num() - 1 > WeaponBowNum) {
					WeaponBowNum++;
				}
				else {
					WeaponBowNum = 0;
				}
			}
			else {
				if (PlayerInvenroty->getWeapon_BowList().Num() > 1) {
					if (WeaponBowNum == 0) {
						WeaponBowNum = PlayerInvenroty->getWeapon_BowList().Num() - 1;
					}
					else {
						WeaponBowNum--;
					}
				}
			}
			Weapon_Bow = PlayerInvenroty->getWeapon_BowList()[WeaponBowNum % PlayerInvenroty->getWeapon_BowList().Num()];
			Weapon_Bow->getWeaponMesh()->SetCollisionProfileName(TEXT("PlayerWeapon"));
			Weapon_Bow->getWeaponMesh()->SetVisibility(true);
			break;
	}
}

void APlayerWeapon::AddSwordList(AWeapon_Equip * sword)
{
	auto PlayerInvenroty = PlayerChracterController->getPlayerCharacter()->getPlayerInvenroty();
	Weapon_Sword->getWeaponMesh()->SetVisibility(false);
	Weapon_Sword = sword;
	PlayerInvenroty->AddSwordInventory(sword);
	WeaponSwordNum++;
}

void APlayerWeapon::AddBowdList(AWeapon_Equip * sword)
{
	auto PlayerInvenroty = PlayerChracterController->getPlayerCharacter()->getPlayerInvenroty();

	Weapon_Bow->getWeaponMesh()->SetVisibility(false);
	Weapon_Bow = sword;
	PlayerInvenroty->AddBowdInventory(sword);
	WeaponBowNum++;
}

void APlayerWeapon::AddShieldList(AWeapon_Equip * sword)
{
	auto PlayerInvenroty = PlayerChracterController->getPlayerCharacter()->getPlayerInvenroty();

	Weapon_Shield->getWeaponMesh()->SetVisibility(false);
	Weapon_Shield = sword;
	PlayerInvenroty->AddShieldInventory(sword);
	WeaponShieldNum ++;
}

void APlayerWeapon::AddArrowBagList(AWeapon_Equip * bag)
{
	ArrowBag = bag;
}

void APlayerWeapon::ChangeWeaponInventory(AWeapon_Equip * weapon)
{
	switch (weapon->getItemTypeValue()) {
	case  ItemTypeValue::ItemType_Weapon:
		Weapon_Sword->getWeaponMesh()->SetVisibility(false);
		Weapon_Sword = weapon;
		break;
	case  ItemTypeValue::ItemType_Bow:
		Weapon_Bow->getWeaponMesh()->SetVisibility(false);
		Weapon_Bow = weapon;
		break;
	case ItemTypeValue::ItemType_Shield:
		Weapon_Shield->getWeaponMesh()->SetVisibility(false);
		Weapon_Shield = weapon;
		break;
	}
}

void APlayerWeapon::setWeaponPlayerCharacter(E_Weapon Weapon)
{
	auto PlayerInvenroty = PlayerChracterController->getPlayerCharacter()->getPlayerInvenroty();

	switch (Weapon) {
	case E_Weapon::Weapon_SWORD:
		if (PlayerInvenroty->getWeapon_SwordList().Num() == 0) {
			Weapon_Sword->getWeaponMesh()->SetVisibility(false);
		}
		else {
			Weapon_Sword = PlayerInvenroty->getWeapon_SwordList()[0];
			setWeaponComponent(E_Weapon::Weapon_None, PlayerChracterController->getPlayerCharacter()->getPlayerMeshComponent()->getRootMesh(), TEXT(""));
		}
		break;
	case E_Weapon::Weapon_BOW:
		if (PlayerInvenroty->getWeapon_BowList().Num() == 0) {
			Weapon_Bow->getWeaponMesh()->SetVisibility(false);
		}
		else {
			Weapon_Bow = PlayerInvenroty->getWeapon_BowList()[0];
			setWeaponComponent(E_Weapon::Weapon_None, PlayerChracterController->getPlayerCharacter()->getPlayerMeshComponent()->getRootMesh(), TEXT(""));
		}
		break;
	case E_Weapon::Weapon_SHIELD:
		if (PlayerInvenroty->getWeapon_ShieldList().Num() == 0) {
			Weapon_Shield->getWeaponMesh()->SetVisibility(false);
		}
		else {
			Weapon_Shield = PlayerInvenroty->getWeapon_ShieldList()[0];
			setWeaponComponent(E_Weapon::Weapon_None, PlayerChracterController->getPlayerCharacter()->getPlayerMeshComponent()->getRootMesh(), TEXT(""));
		}
		break;
	case E_Weapon::Weapon_ARROW_BAG:

		break;
	case E_Weapon::Weapon_None:

		break;
	}
}

bool APlayerWeapon::checkParentComponent(E_Weapon Weapon, const TCHAR * Socket)
{
	bool answer = false;
	FName socketNameString;
	FName NameOfSocket = FName(Socket);
	switch (Weapon) {
	case E_Weapon::Weapon_BOW:
		socketNameString = Weapon_Bow->getWeaponMesh()->GetAttachSocketName();
		if (socketNameString.IsEqual(NameOfSocket)){
			answer = true;
		}
		break;
	case E_Weapon::Weapon_SWORD:
		socketNameString = Weapon_Sword->getWeaponMesh()->GetAttachSocketName();
		if (Socket == socketNameString) {
			answer = true;
		}
		break;
	case E_Weapon::Weapon_SHIELD:
		socketNameString = Weapon_Shield->getWeaponMesh()->GetAttachSocketName();
		if (Socket == socketNameString) {
			answer = true;
		}
		break;
	}
	return answer;
}

void APlayerWeapon::AddArrow()
{
	auto PlayerCharacter = PlayerChracterController->getPlayerCharacter();
	PlayerCharacter->getPlayerUI()->PrintMessageBox("Get 'Arrow x1'");
	ArrowCount++;
}
