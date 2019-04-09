// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ZeldaBOTW.h"
#include "Enemy/BossUIClass.h"
#include "Enemy/EnemyCharacter.h"
#include "GameFramework/Character.h"
#include "BossCharacter.generated.h"

UCLASS()
class ZELDABOTW_API ABossCharacter : public ACharacter
{
	GENERATED_BODY()
private:
	UPROPERTY(Category = Boss, VisibleAnywhere, BlueprintReadOnly, meta = (AllowPrivateAccess = "true"))
	USkeletalMeshComponent* BossHeadMesh;
	UPROPERTY(Category = Boss, EditAnywhere, BlueprintReadOnly, meta = (AllowPrivateAccess = "true"))
	AActor* Treasure;
	UPROPERTY(Category = Boss, VisibleAnywhere, BlueprintReadOnly, meta = (AllowPrivateAccess = "true"))
	TSubclassOf<class UBossUIClass> HDWidgetClass;
	UPROPERTY(Category = Boss, VisibleAnywhere, BlueprintReadOnly, meta = (AllowPrivateAccess = "true"))
	UBossUIClass* BossUI;
	UPROPERTY(Category = Boss, VisibleAnywhere, BlueprintReadOnly, meta = (AllowPrivateAccess = "true"))
	UEnemyCharacterState* BossState;
	float patrolRange;
	float FIndRange;
	float AttackRange;
	bool OnBattle;

protected:
	virtual void BeginPlay() override;

public:
	ABossCharacter();

	virtual void Tick(float DeltaTime) override;
	void setBossHPUI(bool value);
	void HurtBoss();

	bool getOnBattle() { return OnBattle; }
	float getFIndRange() { return FIndRange; }
	float getPatrolRange() { return patrolRange; }
	float getAttackRange() { return AttackRange; }
	void setOnBattle(bool value) { OnBattle = value; }
	USkeletalMeshComponent* getBossHeadMesh() { return BossHeadMesh; }
	FVector getTreasureLotation() { return Treasure->GetActorLocation(); }
};
