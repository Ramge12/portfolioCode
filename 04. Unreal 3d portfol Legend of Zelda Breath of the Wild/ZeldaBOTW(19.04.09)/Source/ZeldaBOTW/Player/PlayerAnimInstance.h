// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ZeldaBOTW.h"
#include "Animation/AnimInstance.h"
#include "PlayerAnimInstance.generated.h"

UCLASS()
class ZELDABOTW_API UPlayerAnimInstance : public UAnimInstance
{
	GENERATED_BODY()
private:
	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Pawn, Meta = (AllowPrivateAccess = true))
	float curSpeed;
	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Pawn, Meta = (AllowPrivateAccess = true))
	bool IsInAir;
	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Pawn, Meta = (AllowPrivateAccess = true))
	bool IsJump;
	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Pawn, Meta = (AllowPrivateAccess = true))
	bool IsMove;
	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Pawn, Meta = (AllowPrivateAccess = true))
	bool IsClimming;
	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Pawn, Meta = (AllowPrivateAccess = true))
	bool IsBattle;
	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Pawn, Meta = (AllowPrivateAccess = true))
	bool IsShoot;
	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Pawn, Meta = (AllowPrivateAccess = true))
	class UPlayerCharacterState* p2PlayerState;

	UPROPERTY(VisibleInstanceOnly, BlueprintReadOnly, Category = Pawn, Meta = (AllowPrivateAccess = true))
	bool IsAttacking;
	UPROPERTY(VisibleInstanceOnly, BlueprintReadOnly, Category = Pawn, Meta = (AllowPrivateAccess = true))
	bool CanNextCombo;
	UPROPERTY(VisibleInstanceOnly, BlueprintReadOnly, Category = Pawn, Meta = (AllowPrivateAccess = true))
	bool IsComboInputOn;
	UPROPERTY(VisibleInstanceOnly, BlueprintReadOnly, Category = Pawn, Meta = (AllowPrivateAccess = true))
	int32 CurrentCombo;
	UPROPERTY(VisibleInstanceOnly, BlueprintReadOnly, Category = Pawn, Meta = (AllowPrivateAccess = true))
	int32 MaxCombo;

	class APlayerCharacterController* PlayerCharacterController;
	bool IsMouseRightButtonDown = false;
public:
	UPlayerAnimInstance();

	void AttackStartComboState();
	void AttackEndComboState();
	void StartAttackCollision();

	virtual void NativeBeginPlay() override;
	virtual void NativeUpdateAnimation(float DeltaSeconds) override;

private:
	UFUNCTION()
		void AnimNotify_HurtEnd();
	UFUNCTION()
		void AnimNotify_NextAttackCheck();
	UFUNCTION()
		void AnimNotify_AttackComboEnd();
	UFUNCTION()
		void AnimNotify_JumpStartFinish();
	UFUNCTION()
		void AnimNotify_JumpEnd();
	UFUNCTION()
		void AnimNotify_StartBow();
	UFUNCTION()
		void AnimNotify_EndBow();
	UFUNCTION()
		void AnimNotify_StartAIM();
	UFUNCTION()
		void AnimNotify_DrawSwordEnd();
	UFUNCTION()
		void AnimNotify_StealthSword();
	UFUNCTION()
		void AnimNotify_ThrowEnd();
	UFUNCTION()
		void AnimNotify_CounterAttackEnd();

};
