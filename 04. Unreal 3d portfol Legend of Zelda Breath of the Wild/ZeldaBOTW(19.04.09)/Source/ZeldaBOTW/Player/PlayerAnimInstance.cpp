// Fill out your copyright notice in the Description page of Project Settings.

#include "PlayerAnimInstance.h"
#include "Player/PlayerCharacter.h"
#include "Player/Controller/PlayerCharacterController.h"
#include "Player/PlayerCharacterState.h"

UPlayerAnimInstance::UPlayerAnimInstance()
{
	IsInAir = false;
	IsJump = false;
	IsMove = false;
	IsShoot = false;
	IsClimming = false;
	IsMouseRightButtonDown = false;
	MaxCombo = 3;
}

void UPlayerAnimInstance::AttackStartComboState()
{
	CanNextCombo = true;
	IsComboInputOn = false;
	CurrentCombo = FMath::Clamp<int32>(CurrentCombo + 1, 1, MaxCombo);
	StartAttackCollision();
}

void UPlayerAnimInstance::AttackEndComboState()
{
	if (IsValid(PlayerCharacterController)) {
		auto PlayerState = Cast<APlayerCharacterController>(PlayerCharacterController)->getPlayerState();
		PlayerState->setPlayerState(E_PlayerState::Player_IDLE);
	}
	IsComboInputOn = false;
	CanNextCombo = false;
	IsAttacking = false;
	CurrentCombo = 0;
}

void UPlayerAnimInstance::StartAttackCollision()
{
	auto PlayerSworld = PlayerCharacterController->getPlayerWeapon()->getPlayerSworld();
	if (PlayerSworld) {
		PlayerSworld->setHitEnemy(true);
	}
}

void UPlayerAnimInstance::NativeBeginPlay()
{
	Super::NativeBeginPlay();
	PlayerCharacterController = Cast< APlayerCharacterController>(GetWorld()->GetFirstPlayerController());
	if (IsValid(PlayerCharacterController)) {
		p2PlayerState = PlayerCharacterController->getPlayerState();
		AttackEndComboState();
	}
}

void UPlayerAnimInstance::NativeUpdateAnimation(float DeltaSeconds)
{
	Super::NativeUpdateAnimation(DeltaSeconds);
	if (IsValid(PlayerCharacterController)) {
		IsMove = PlayerCharacterController->getMoveController()->getIsMove();
		IsClimming = PlayerCharacterController->getMoveController()->getIsClimming();
		IsBattle = PlayerCharacterController->getBattleController()->getIsBattle();
		IsShoot = PlayerCharacterController->getBattleController()->getIsShoot();
		auto playerState = PlayerCharacterController->getPlayerState();
		if (playerState) {
			p2PlayerState = playerState;
		}
	}
	
	auto Pawn = TryGetPawnOwner();
	if (::IsValid(Pawn)) {
		curSpeed = Pawn->GetVelocity().Size();
		auto Character = Cast<ACharacter>(Pawn);
		if (Character) {
			IsInAir = Character->GetMovementComponent()->IsFalling();
		}
	}
	if (IsValid(PlayerCharacterController)) {
		if (!IsMouseRightButtonDown && PlayerCharacterController->getBattleController()->getIsSword() && p2PlayerState->CheckCurState(E_PlayerState::Player_SWORD) && PlayerCharacterController->IsInputKeyDown(EKeys::LeftMouseButton)) {
			IsMouseRightButtonDown = true;
			if (IsAttacking) {
				ABCHECK(FMath::IsWithinInclusive<int32>(CurrentCombo, 1, MaxCombo));
				if (CanNextCombo) {
					IsComboInputOn = true;
				}
			}
			else {
				ABCHECK(CurrentCombo == 0);
				AttackStartComboState();
				IsAttacking = true;
			}
		}
		else if (!PlayerCharacterController->IsInputKeyDown(EKeys::LeftMouseButton)) {
			IsMouseRightButtonDown = false;
		}
	}
}

void UPlayerAnimInstance::AnimNotify_HurtEnd()
{
	if (IsValid(PlayerCharacterController)) {
		p2PlayerState->setPlayerState(E_PlayerState::Player_IDLE);
	}
}

void UPlayerAnimInstance::AnimNotify_NextAttackCheck()
{
	CanNextCombo = false;
	if (IsComboInputOn) {
		AttackStartComboState();
	}
	else {
		AttackEndComboState();
	}
	if (IsValid(PlayerCharacterController)) {
		auto PlayerSworld = PlayerCharacterController->getPlayerWeapon()->getPlayerSworld();
		PlayerSworld->setHitEnemy(false);
	}
	StartAttackCollision();
}

void UPlayerAnimInstance::AnimNotify_AttackComboEnd()
{
	AttackEndComboState();
	if (IsValid(PlayerCharacterController)) {
		auto PlayerSworld = PlayerCharacterController->getPlayerWeapon()->getPlayerSworld();
		PlayerSworld->setHitEnemy(false);
	}
}

void UPlayerAnimInstance::AnimNotify_JumpStartFinish()
{
	IsJump = true;
}

void UPlayerAnimInstance::AnimNotify_JumpEnd()
{
	IsJump = false;
}

void UPlayerAnimInstance::AnimNotify_StartBow()
{
	if (IsValid( PlayerCharacterController)) {
		//auto PlayerState = PlayerCharacterController->getPlayerState();
		if (p2PlayerState->CheckCurState(E_PlayerState::Player_SHOT)) {
			PlayerCharacterController->getBattleController()->setIsShoot(true);
			if (!PlayerCharacterController->IsInputKeyDown(EKeys::RightMouseButton)) {
				p2PlayerState->setPlayerState(p2PlayerState->getPrePlayerState());
			}
		}
	}
}

void UPlayerAnimInstance::AnimNotify_EndBow()
{
	if (IsValid(PlayerCharacterController)) {
		//auto PlayerState = PlayerCharacterController->getPlayerState();
		auto Pawn = TryGetPawnOwner();
		auto Character = Cast<APlayerCharacter>(Pawn);
		if (p2PlayerState->CheckCurState(E_PlayerState::Player_SHOT)) {
			p2PlayerState->setPlayerState(p2PlayerState->getPrePlayerState());
			PlayerCharacterController->getPlayerWeapon()->setWeaponComponent(E_Weapon::Weapon_BOW, Character->getPlayerMeshComponent()->getRootMesh(), TEXT("BackWaistSocket"));
		}
	}
}

void UPlayerAnimInstance::AnimNotify_StartAIM()
{
	if (IsValid(PlayerCharacterController)) {
		PlayerCharacterController->getBattleController()->setCanShootArrow(true);
		PlayerCharacterController->getPlayerCharacter()->getPlayerUI()->SetPlayerAim(true);
	}
}

void UPlayerAnimInstance::AnimNotify_DrawSwordEnd()
{
	if (IsValid(PlayerCharacterController)) {
		if (p2PlayerState->getPrePlayerState() != E_PlayerState::Player_SHOPPING && !p2PlayerState->CheckCurState(E_PlayerState::Player_SHOPPING)) {
			p2PlayerState->setPlayerState(E_PlayerState::Player_IDLE);
		}
		else {
			p2PlayerState->setPlayerState(E_PlayerState::Player_SHOPPING);
		}
		PlayerCharacterController->getBattleController()->setIsSword(true);
	}
}

void UPlayerAnimInstance::AnimNotify_StealthSword()
{
	if (PlayerCharacterController) {
		//auto PlayerState = PlayerCharacterController->getPlayerState();
		auto Pawn = TryGetPawnOwner();
		auto Character = Cast<APlayerCharacter>(Pawn);
		if (p2PlayerState->CheckCurState(E_PlayerState::Player_SWORD)) {
			p2PlayerState->setPlayerState(p2PlayerState->getPrePlayerState());
			PlayerCharacterController->getPlayerWeapon()->setWeaponComponent(E_Weapon::Weapon_None, Character->getPlayerMeshComponent()->getRootMesh(), TEXT("LeftUpLegSocket"));
		}
	}
}

void UPlayerAnimInstance::AnimNotify_ThrowEnd()
{
	if (PlayerCharacterController) {
		//UPlayerCharacterState* PlayerState = PlayerCharacterController->getPlayerState();
		p2PlayerState->setThrowObject(false);
		p2PlayerState->setPlayerState(E_PlayerState::Player_IDLE);
	}
}

void UPlayerAnimInstance::AnimNotify_CounterAttackEnd()
{
	if (PlayerCharacterController) {
		p2PlayerState->setCounterAttackValue(false);
		p2PlayerState->setCounterPossibility(false);
		p2PlayerState->setPlayerState(p2PlayerState->getPrePlayerState());
		PlayerCharacterController->getBattleController()->CounterAttackEnemy();
	}
}
