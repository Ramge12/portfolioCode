// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ZeldaBOTW.h"
#include "Weapon/Weapon_Arrow.h"
#include "Weapon/Weapon_Equip.h"
#include "GameFramework/Actor.h"
#include "PlayerWeapon.generated.h"

UENUM()
enum class E_Weapon : uint8 {
	Weapon_None			UMETA(DisplayName = "Weapon_NONE"),
	Weapon_BOW			UMETA(DisplayName = "Weapon_BOW"),
	Weapon_ARROW_BAG	UMETA(DisplayName = "Weapon_ARROW_BAG"),
	Weapon_SWORD		UMETA(DisplayName = "Weapon_SWORD"),
	Weapon_SHIELD		UMETA(DisplayName = "Weapon_SHIELD")
};

UCLASS()
class ZELDABOTW_API APlayerWeapon : public AActor
{
	GENERATED_BODY()

private:
	AWeapon_Equip* ArrowBag;
	AWeapon_Equip* Weapon_Bow;
	AWeapon_Equip* Weapon_Sword;
	AWeapon_Equip* Weapon_Shield;

	UStaticMeshComponent* Show_Arrow;
	TSubclassOf<class AWeapon_Arrow> Weapon_Arrows;
	class APlayerCharacterController* PlayerChracterController;

	int32 ArrowCount = 0;
	int32 WeaponBowNum = 0;
	int32 WeaponSwordNum = 0;
	int32 WeaponShieldNum = 0;

protected:
	virtual void BeginPlay() override;

public:	
	APlayerWeapon();

public:	
	void setWeaponComponent(E_Weapon Weapon, USkeletalMeshComponent* MeshComponent, const TCHAR * Socket);
	void FireArrow(FVector location, FRotator Rotation, FVector Forward);

	void ShowArrowLocationRotation(FVector Location, FRotator Rotation, bool aimOnOFf);

	void ChangeWeapon(E_Weapon weaponType,bool Upvalue);
	void AddSwordList(AWeapon_Equip* sword);
	void AddBowdList(AWeapon_Equip* sword);
	void AddShieldList(AWeapon_Equip* sword);
	void AddArrowBagList(AWeapon_Equip* bag);
	void ChangeWeaponInventory(AWeapon_Equip* weapon);

	void setWeaponPlayerCharacter(E_Weapon Weapon);

	bool checkParentComponent(E_Weapon Weapon, const TCHAR * Socket);
	void AddArrow();
	AWeapon_Equip* getPlayerArrowBag() { return ArrowBag; }
	AWeapon_Equip* getPlayerSworld() { return Weapon_Sword; }
	AWeapon_Equip* getPlayerShield() { return Weapon_Shield; }
	AWeapon_Equip* getPlayerBow() { return Weapon_Bow; }
};
