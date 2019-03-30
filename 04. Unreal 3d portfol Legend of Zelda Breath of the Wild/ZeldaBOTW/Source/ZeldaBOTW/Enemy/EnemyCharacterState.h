// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ZeldaBOTW.h"
#include "Components/ActorComponent.h"
#include "EnemyCharacterState.generated.h"

UENUM()
enum class EnemyWeaponType : uint8 {
	Weapon_Sword	UMETA(DisplayName = "Weapon_Sword"),
	Weapon_Bow		UMETA(DisplayName = "Weapon_Bow")
};

UENUM()
enum class EnemyState : uint8 {
	Enemy_IDLE		UMETA(DisplayName = "Enemy_IDLE"),
	Enemy_RUN		UMETA(DisplayName = "Enemy_RUN"),
	Enemy_ATTACK	UMETA(DisplayName = "Enemy_ATTACK"),
	Enemy_HURT		UMETA(DisplayName = "Enemy_HURT"),
	Enemy_DEATH		UMETA(DisplayName = "Enemy_DEATH")
};

UCLASS( ClassGroup=(Custom), meta=(BlueprintSpawnableComponent) )
class ZELDABOTW_API UEnemyCharacterState : public UActorComponent
{
	GENERATED_BODY()

private:
	UPROPERTY(Category = STATE, EditAnywhere, BlueprintReadWrite, meta = (AllowPrivateAccess = "true"))
		EnemyWeaponType EnemyCharacterWeaponType;
	UPROPERTY(Category = STATE, EditAnywhere, BlueprintReadWrite, meta = (AllowPrivateAccess = "true"))
		EnemyState curEnemyState;
	UPROPERTY(Category = STATE, EditAnywhere, BlueprintReadWrite, meta = (AllowPrivateAccess = "true"))
		float IdleRange = 0.0f;
	UPROPERTY(Category = STATE, EditAnywhere, BlueprintReadWrite, meta = (AllowPrivateAccess = "true"))
		float AttackRange = 0.0f;
	UPROPERTY(Category = STATE, EditAnywhere, BlueprintReadWrite, meta = (AllowPrivateAccess = "true"))
		float MaxAttackRange = 0.0f;
	UPROPERTY(Category = STATE, EditAnywhere, BlueprintReadWrite, meta = (AllowPrivateAccess = "true"))
		float CurrentChracterSpeed = 0.0f;
	UPROPERTY(Category = STATE, EditAnywhere, BlueprintReadWrite, meta = (AllowPrivateAccess = "true"))
		int32 IdleNumber = 1;
	UPROPERTY(Category = STATE, EditAnywhere, BlueprintReadWrite, meta = (AllowPrivateAccess = "true"))
		bool OnBattle;

	bool EnemyMaxRange;
	bool InAttackRange;
	bool hasWeapon;

	bool AttackDelay;
	float AttackDelayTimer;
	FTimerHandle AttackDelayTimerHandler;


	UPROPERTY(Category = STATE, EditAnywhere, BlueprintReadWrite, meta = (AllowPrivateAccess = "true"))
	int32 enemyCurHp;

	int32 enemyMaxHp;
	int32 enemyATK;

public:
	UEnemyCharacterState();

protected:
	virtual void BeginPlay() override;

public:
	virtual void TickComponent(float DeltaTime, ELevelTick TickType, FActorComponentTickFunction* ThisTickFunction) override;
	void HurtEnemy();
	void AttackDelayFuncition();
	void DelayTimer();

	EnemyWeaponType getEnemyCharacterKind() { return EnemyCharacterWeaponType; }
	EnemyState getCurEnemyState() { return curEnemyState; }

	float getEnemyAttackRange() { return AttackRange; }
	float getIdleRange() { return IdleRange; }
	float getMaxRage() { return MaxAttackRange; }
	float getMaxHp() { return enemyMaxHp; }
	float getCurHp() { return enemyCurHp; }
	bool getEnemyMaxRage() { return EnemyMaxRange; }
	bool getOnBattle() { return OnBattle; }
	bool getHasWeapon() { return hasWeapon; }
	bool getInAttackRange() { return InAttackRange; }

	void setMaxHP(float value);
	void setEnemyMaxRange(bool value) { EnemyMaxRange = value; }
	void setIdleNumber(int32 value) { IdleNumber = value; }
	void setOnBattle(bool value) { OnBattle = value; }
	void setHasWeapon(bool value) { hasWeapon = value; }
	void setInAttackRange(bool value) { InAttackRange = value; }
	void setCurEnemyState(EnemyState value) { curEnemyState = value; }
};