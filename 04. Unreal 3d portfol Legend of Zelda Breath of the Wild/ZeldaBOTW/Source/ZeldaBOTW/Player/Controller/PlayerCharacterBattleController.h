// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ZeldaBOTW.h"
#include "Player/PlayerWeapon.h"
#include "Weapon/Weapon_Arrow.h"
#include "GameFramework/PlayerController.h"
#include "PlayerCharacterBattleController.generated.h"

UCLASS()
class ZELDABOTW_API APlayerCharacterBattleController : public APlayerController
{
	GENERATED_BODY()
private:
	UPROPERTY(Category = Character, VisibleAnywhere, BlueprintReadOnly, meta = (AllowPrivateAccess = "true"))
	class APlayerCharacter* PlayerCharacter;
	UPROPERTY(Category = Character, VisibleAnywhere, BlueprintReadOnly, meta = (AllowPrivateAccess = "true"))
	class UPlayerCharacterState* PlayerCharacterState;
	UPROPERTY(Category = Character, VisibleAnywhere, BlueprintReadOnly, meta = (AllowPrivateAccess = "true"))
	APlayerWeapon* PlayerWeapon;
	UPROPERTY(Category = Character, VisibleAnywhere, BlueprintReadOnly, meta = (AllowPrivateAccess = "true"))
	class UPlayerTrace* PlayerTrace;
private:
	bool IsBattle;
	bool IsShoot;
	bool CanShootArrow;
	bool IsSword;
	int32 WeaponIdleTime;
	FTimerHandle AttackTimerHandle;
public:
	APlayerCharacterBattleController();
	void PlayerBattleSetting(class APlayerCharacter* Character, class UPlayerCharacterState* Sate,class UPlayerTrace* Trace);

	void PlayerShotStart();
	void PlayerShotEnd();
	void PlayerAttackStart();

	void PlayerHurt();

	void ShootArrow();
	void WeaponToIdleTimer();
	void WeaponToIdle();

	void WeaponChangeIDLE();
	void WeaponChangeSword();
	void WeaponChangeBow();

	void CounterAttackEnemy();

	void PlayerWeaponChangeLeft();
	void PlayerWeaponChangeRight();

	bool getIsSword() { return IsSword; }
	bool getIsShoot() { return IsShoot; }
	bool getIsBattle() { return IsBattle; }
	bool getCanShootArrow() { return CanShootArrow; }

	void setIsSword(bool value) { IsSword = value; }
	void setIsShoot(bool value) { IsShoot = value; }
	void setCanShootArrow(bool value) { CanShootArrow = value; }
	void setPlayerWeapon(APlayerWeapon* Weapon) { PlayerWeapon = Weapon; }
};
