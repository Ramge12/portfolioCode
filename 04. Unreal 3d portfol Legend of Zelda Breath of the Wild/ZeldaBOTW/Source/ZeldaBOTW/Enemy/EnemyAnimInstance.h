// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "Animation/AnimInstance.h"
#include "EnemyAnimInstance.generated.h"


UCLASS()
class ZELDABOTW_API UEnemyAnimInstance : public UAnimInstance
{
	GENERATED_BODY()
	
private:
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Pawn, Meta = (AllowPrivateAccess = true))
	class UEnemyCharacterState* EnemyState;
public:
	UEnemyAnimInstance();

	UFUNCTION()
		void setEnemyState(class UEnemyCharacterState* state) { EnemyState = state; }
	UFUNCTION()
		void AnimNotify_AttackStart();
	UFUNCTION()
		void AnimNotify_AttackEnd();
	UFUNCTION()
		void AnimNotify_HurtEnd();
	UFUNCTION()
		void AnimNotify_ArrowShotEnd();

	
	
};
