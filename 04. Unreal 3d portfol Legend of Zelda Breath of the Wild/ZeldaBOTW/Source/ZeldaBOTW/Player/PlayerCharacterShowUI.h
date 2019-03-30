// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ZeldaBOTW.h"

#include "Components/SceneCaptureComponent2D.h"
#include "Weapon/Weapon_Equip.h"
#include "GameFramework/Pawn.h"
#include "PlayerCharacterShowUI.generated.h"

UCLASS()
class ZELDABOTW_API APlayerCharacterShowUI : public APawn
{
	GENERATED_BODY()
public:
	UCameraComponent* PlayerUICamera;
	USpringArmComponent* SpringArm;
	USceneCaptureComponent2D* playerCaputre;

	UStaticMeshComponent* Weapon_Bow;
	UStaticMeshComponent* Weapon_Shield;
	UStaticMeshComponent* Weapon_Sword;
	UStaticMeshComponent* ArrowBag;

	USkeletalMeshComponent* faceMesh;
	USkeletalMeshComponent* hairMesh;
	USkeletalMeshComponent* upperMesh;
	USkeletalMeshComponent* underMesh;

public:
	APlayerCharacterShowUI();

	virtual void Tick(float DeltaTime) override;

	void SetSkeletalMeshComponent(USkeletalMeshComponent* skeletalMesh, const TCHAR* Channel);
	void SetAnimClass(const TCHAR * AnimChannel);
	void SetMeshPosition(float X, float Y, float Z);
	
	void SetWeaponUI();
	void SetWeaponPlayerUI(UStaticMeshComponent* weapon, AWeapon_Equip* playerWeapon);
};
