// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ZeldaBOTW.h"
#include "Object/ItemClass.h"
#include "Weapon_Equip.generated.h"

UENUM()
enum class WeaponType : uint8 {
	WeaponType_Sword		UMETA(DisplayName = "WeaponType_Sword"),
	WeaponType_Bow			UMETA(DisplayName = "WeaponType_Bow"),
	WeaponType_Shield		UMETA(DisplayName = "WeaponType_Shield"),
	WeaponType_ArrowBag		UMETA(DisplayName = "WaeponType_ArrowBag")
};

UCLASS()
class ZELDABOTW_API AWeapon_Equip : public AItemClass
{
	GENERATED_BODY()
private:
	bool hitEnemy;
	bool hitPlayer;
public:
	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Weapon, Meta = (AllowPrivateAccess = true))
		WeaponType CurWeaponType;
	UPROPERTY(VisibleAnywhere, Category = Weapon)
	UStaticMeshComponent* WeaponMesh;

protected:
	virtual void BeginPlay() override;
public:	
	AWeapon_Equip();

	UFUNCTION()
	void WeaponCollision(UPrimitiveComponent* OverlappedComponent, AActor* OtherActor, UPrimitiveComponent* OtherComp, int32 OtherBodyIndex, bool bFromSweep, const FHitResult & SweepResult);
	void setCharacterEquip(USkeletalMeshComponent* MeshComponent, const TCHAR * Socket);

	UStaticMeshComponent* getWeaponMesh() { return WeaponMesh; }
	WeaponType getCurWeaponType() { return CurWeaponType; }

	void deleteWeaponMesh();
	void setHitEnemy(bool value) { hitEnemy = value; }
	void setHitPlayer(bool value) { hitPlayer = value; }
	void setDropItemInformation(AItemClass* info);
	
};
