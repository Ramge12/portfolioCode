#include "PlayerChracterMoveController.h"
#include "PlayerCharacterController.h"
#include "Player/PlayerCharacter.h"
#include "Player/PlayerCharacterState.h"
#include "Player/PlayerTrace.h"

APlayerChracterMoveController::APlayerChracterMoveController()
{
	IsJump = false;
	IsMove = false;
	IsPush = false;
	IsClimming = false;
	playerSpeed = 0.5f;
	playerCurSpeed = playerSpeed;
}

void APlayerChracterMoveController::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);
	PlayerJumpCheck();
	PlayerStaminCheck();
	PlayerTracePushCheck();
	PlayerTraceClimmingCheck();
	PlayerTrace->PlayerTraceCheck(PlayerCharacter->GetActorLocation(), PlayerCharacter->GetActorForwardVector(), PlayerCharacter);
}

void APlayerChracterMoveController::PlayerMoveSetting(APlayerCharacter * Character, UPlayerCharacterState * Sate, UPlayerTrace * Trace)
{
	PlayerCharacter=Character;
	PlayerCharacterState= Sate;
	PlayerTrace= Trace;
}

void APlayerChracterMoveController::KeyDownCheck()
{
	if (PlayerCharacterState->CheckCurState(E_PlayerState::Player_WALK) || PlayerCharacterState->CheckCurState(E_PlayerState::Player_RUN)) {
		if (!PlayerCharacterState->CheckCurState(E_PlayerState::Player_SHOT)&& !PlayerCharacterState->CheckCurState(E_PlayerState::Player_SHOPPING)) {
			PlayerCharacter->GetCharacterMovement()->SetMovementMode(EMovementMode::MOVE_Walking);
			PlayerCharacterState->setPlayerState(E_PlayerState::Player_IDLE);
		}
	}
	IsMove = false;
	playerCurSpeed = playerSpeed;
}

void APlayerChracterMoveController::PlayerStaminCheck()
{
	float curSpeed = PlayerCharacter->GetVelocity().Size();
	if (IsValid(PlayerCharacterState)) {
		if (PlayerCharacterState->CheckCurState(E_PlayerState::Player_RUN)) {
			PlayerCharacterState->getStaminaUseSet(true);
			if (PlayerCharacterState->getStaminaPercent() < 0.1f) {
				PlayerRunStop();
				PlayerCharacterState->setStaminaCharge(true);
			}
		}
		else if (PlayerCharacterState->CheckCurState(E_PlayerState::Player_PUSH)) {

			if (curSpeed < 0.1f) PlayerCharacterState->getStaminaUseSet(false);
			else PlayerCharacterState->getStaminaUseSet(true);

			if (PlayerCharacterState->getStaminaPercent() < 0.1f) {
				PlayerPushEnd();
				PlayerCharacterState->setStaminaCharge(true);
			}
		}
		else if (PlayerCharacterState->CheckCurState(E_PlayerState::Player_CLIMING)) {
			PlayerCharacterState->getStaminaUseSet(true);
			if (curSpeed > 0.1f) PlayerCharacterState->StaminaSetting(-GetWorld()->GetDeltaSeconds() / 50.0f);

			if (PlayerCharacterState->getStaminaPercent() < 0.1f) {
				PlayerClimmingEnd();
				PlayerCharacterState->setStaminaCharge(true);
			}
		}
		else {
			PlayerCharacterState->getStaminaUseSet(false);
		}

		if (PlayerCharacterState->getStaminaPercent() < 1.0f) {
			PlayerCharacter->getPlayerUI()->ShowStamina(true);
		}
		else {
			PlayerCharacter->getPlayerUI()->ShowStamina(false);
		}
	}
}

void APlayerChracterMoveController::PlayerJumpCheck()
{
	if (IsJump){
		if (PlayerCharacter->GetMovementComponent()->IsFalling() && !PlayerCharacterState->CheckCurState(E_PlayerState::Player_JUMP)) {
			PlayerCharacterState->setPlayerState(E_PlayerState::Player_JUMP);
		}
		else if (PlayerCharacter->GetMovementComponent()->IsMovingOnGround() && PlayerCharacterState->CheckCurState(E_PlayerState::Player_JUMP)) {
			PlayerCharacterState->setPlayerState(PlayerCharacterState->getPrePlayerState());
			IsJump = false;
		}
	}
}

void APlayerChracterMoveController::PlayerJump()
{
	if (PlayerCharacterState->CheckCurState(E_PlayerState::Player_IDLE) || PlayerCharacterState->CheckCurState(E_PlayerState::Player_WALK) || PlayerCharacterState->CheckCurState(E_PlayerState::Player_RUN) || PlayerCharacterState->CheckCurState(E_PlayerState::Player_CLIMING) ){
		PlayerCharacter->GetCharacterMovement()->SetMovementMode(EMovementMode::MOVE_Walking);
		PlayerCharacter->Jump();
		PlayerCharacterState->StaminaSetting(-0.1f);
		IsJump = true;
	}
}

void APlayerChracterMoveController::PlayerRunStart()
{
	//if (IsValid(PlayerCharacterState)) {
		if (!PlayerCharacterState->getStaminaCharge() && PlayerCharacterState->CheckCurState(E_PlayerState::Player_WALK)) {
			PlayerCharacterState->CheckState_and_setState(E_PlayerState::Player_WALK, E_PlayerState::Player_RUN);
			playerCurSpeed = playerSpeed * 1.5f;
		}
	//}
}

void APlayerChracterMoveController::PlayerRunStop()
{
	if (PlayerCharacterState->CheckCurState(E_PlayerState::Player_RUN))
	{
		PlayerCharacterState->CheckState_and_setState(E_PlayerState::Player_RUN, E_PlayerState::Player_WALK);
		playerCurSpeed = playerSpeed;
	}
}

void APlayerChracterMoveController::PlayerCrouch()
{
	//if (IsValid(PlayerCharacterState)) {
		if (PlayerCharacterState->CheckCurState(E_PlayerState::Player_IDLE) ||
			PlayerCharacterState->CheckCurState(E_PlayerState::Player_WALK) ||
			PlayerCharacterState->CheckCurState(E_PlayerState::Player_RUN))
		{
			PlayerCharacterState->setPlayerState(E_PlayerState::Player_CROUCH);
			PlayerCharacter->Crouch();
		}
	//}
}

void APlayerChracterMoveController::PlayerUnCrouch()
{
	if (PlayerCharacterState->CheckCurState(E_PlayerState::Player_CROUCH)) {
		PlayerCharacter->UnCrouch();
		PlayerCharacterState->setPlayerState(E_PlayerState::Player_IDLE);
	}
}

void APlayerChracterMoveController::PlayerTracePushCheck()
{
	if (PlayerCharacterState->CheckCurState(E_PlayerState::Player_PUSH) || PlayerCharacterState->CheckCurState(E_PlayerState::Player_WALK) || PlayerCharacterState->CheckCurState(E_PlayerState::Player_RUN)) {
		if (!PlayerCharacterState->getStaminaCharge() && PlayerTrace->getTracePush() && PlayerTrace->getTraceClose() && PlayerTrace->getTraceForward() && !IsPush) {
			PlayerPushStart();
		}
		else if (!PlayerTrace->getTracePush() && !PlayerTrace->getTraceClose() && !PlayerTrace->getTraceForward() && IsPush) {
			PlayerPushEnd();
		}
	}
}

void APlayerChracterMoveController::PlayerPushStart()
{
	PlayerCharacterState->setPlayerState(E_PlayerState::Player_PUSH);
	PlayerCharacter->GetMesh()->SetRelativeLocation(FVector(-15.0f, 0, -32.0f));
	IsPush = true;
}

void APlayerChracterMoveController::PlayerPushEnd()
{
	PlayerCharacterState->setPlayerState(PlayerCharacterState->getPrePlayerState());
	PlayerCharacter->GetMesh()->SetRelativeLocation(FVector(0, 0, -32.0f));
	IsPush = false;
}

void APlayerChracterMoveController::PlayerTraceClimmingCheck()
{
	if (PlayerCharacterState->CheckCurState(E_PlayerState::Player_CLIMING) || PlayerCharacterState->CheckCurState(E_PlayerState::Player_WALK) || PlayerCharacterState->CheckCurState(E_PlayerState::Player_RUN) || PlayerCharacterState->CheckCurState(E_PlayerState::Player_JUMP)) {
		if (!PlayerCharacterState->getStaminaCharge() && PlayerTrace->getTraceHeight() && PlayerTrace->getTraceClimming() && PlayerTrace->getTraceClose() && PlayerTrace->getTraceForward() && !IsClimming) {
			PlayerClimmingStart();
		}
		else if (!PlayerTrace->getTraceHeight() && PlayerTrace->getTraceClimming() && PlayerTrace->getTraceClose() && PlayerTrace->getTraceForward() && IsClimming) {
			IsClimming = false;
		}
		if (!PlayerTrace->getTraceClose() || !PlayerTrace->getTraceForward()) {
			if (PlayerCharacterState->CheckCurState(E_PlayerState::Player_CLIMING)) {
				PlayerClimmingEnd();
			}
		}
	}
	if (PlayerCharacterState->CheckCurState(E_PlayerState::Player_CLIMING) && !IsClimming) {
		PlayerCharacter->SetActorLocation(PlayerCharacter->GetActorLocation() + PlayerCharacter->GetActorUpVector()*0.75f);
	}
}

void APlayerChracterMoveController::PlayerClimmingStart()
{
	PlayerCharacterState->setPlayerState(E_PlayerState::Player_CLIMING);
	PlayerCharacter->GetCharacterMovement()->StopMovementImmediately();
	PlayerCharacter->GetCharacterMovement()->SetMovementMode(EMovementMode::MOVE_Flying);
	IsClimming = true;
}

void APlayerChracterMoveController::PlayerClimmingEnd()
{
	PlayerCharacterState->setPlayerState(PlayerCharacterState->getPrePlayerState());
	PlayerCharacter->GetCharacterMovement()->SetMovementMode(EMovementMode::MOVE_Walking);
	PlayerCharacterState->setPlayerState(E_PlayerState::Player_IDLE);
	IsClimming = false;
	playerCurSpeed = playerSpeed;
	PlayerCharacter->SetActorLocation(PlayerCharacter->GetActorLocation() + PlayerCharacter->GetActorForwardVector()*2.0f);
}

void APlayerChracterMoveController::PlayerExtraState()
{
	auto PlayerController = GetWorld()->GetFirstPlayerController();
	auto PController = Cast< APlayerCharacterController>(PlayerController);
	if (IsValid(PlayerCharacterState)) {
		if (PlayerCharacterState->CheckCurState(E_PlayerState::Player_Carry)) {
			PlayerTrace->ThrowObject();
		}
		else if (PlayerCharacterState->getPlayerExtraState() == (E_PlayerState::Player_SHOPPING)) {
			if (PlayerCharacterState->CheckCurState(E_PlayerState::Player_SHOPPING)) {
			//	PController->MouseOn();
				PlayerCharacterState->setPlayerState(E_PlayerState::Player_IDLE);
				PController->getPlayerCharacter()->getPlayerUI()->ShowShopButton(false);
				if (PController->getPlayerCharacter()->getPlayerUI()->getInvenrotyActive()) {
					PController->getPlayerCharacter()->getPlayerUI()->ShowInventory();
				}
				else {
					PController->MouseOn();
				}
			}
			else {
				PController->MouseOn();
				PlayerCharacterState->setExtraState();
			}
		}
		else if (PlayerCharacterState->getPlayerExtraState() != E_PlayerState::Player_NONE) {
			PlayerCharacterState->setExtraState();
		}
	}
}
