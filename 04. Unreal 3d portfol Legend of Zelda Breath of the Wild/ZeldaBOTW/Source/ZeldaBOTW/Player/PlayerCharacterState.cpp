#include "PlayerCharacterState.h"
#include "Player/Controller/PlayerCharacterController.h"

UPlayerCharacterState::UPlayerCharacterState()
{
	PrimaryComponentTick.bCanEverTick = true;
	EPlayerState = E_PlayerState::Player_IDLE;
	preEPlayerState = E_PlayerState::Player_CROUCH;
	playerExtraState = E_PlayerState::Player_NONE;
	playerCurHp = 0;
	playerMaxHp = 12;
	playerATK = 10;
	invincibleTime = 1.5f;
	invincible = false;
	CounterAttack = false;
	staminValue = 5.0f;
	StaminCharge = false;

	playerCurHp = playerMaxHp;
}

void UPlayerCharacterState::TickComponent(float DeltaTime, ELevelTick TickType, FActorComponentTickFunction * ThisTickFunction)
{
	Super::TickComponent(DeltaTime, TickType, ThisTickFunction);
	if (!StaminaUse) {
		if (staminValue < 1.0f) {
			staminValue += DeltaTime / 10.0f;
		}
		else {
			staminValue = 1.0f;
			StaminCharge = false;
		}
	}
	else {
		if (staminValue > 0.0f) {
			staminValue -= DeltaTime / 100.0f;
		}
		else {
			staminValue = 0.0f;
			StaminCharge = true;
		}
	}
}

void UPlayerCharacterState::setPlayerState(E_PlayerState setValue)
{
	if (EPlayerState != setValue) {
		preEPlayerState = EPlayerState;
		EPlayerState = setValue;
	}
}

bool UPlayerCharacterState::CheckCurState(E_PlayerState setValue)
{
	if (this == nullptr)return false;
	bool answer = false;
	if (setValue == EPlayerState)answer = true;
	return answer;
}

void UPlayerCharacterState::CheckState_and_setState(E_PlayerState isCheckValue, E_PlayerState setValue)
{
	if (EPlayerState == isCheckValue) {
		setPlayerState(setValue);
	}
}

void UPlayerCharacterState::setPlayerCurHP(int32 value)
{
	if (!CheckCurState(E_PlayerState::Player_DEATH)) {
		if (!invincible) {
			playerCurHp += value;
			if (playerCurHp < 0)playerCurHp = 0;
			if (playerCurHp > playerMaxHp)playerCurHp = playerMaxHp;
		}
		if (playerCurHp <= 0) {
			setPlayerState(E_PlayerState::Player_DEATH);
		}
		else if (CheckCurState(E_PlayerState::Player_IDLE)|| CheckCurState(E_PlayerState::Player_WALK)|| CheckCurState(E_PlayerState::Player_RUN)) {
			setPlayerState(E_PlayerState::Player_HURT);
		}
		if (value < 0 && !invincible) {
			GetWorld()->GetTimerManager().ClearTimer(invincibleTimerHandle);
			GetWorld()->GetTimerManager().SetTimer(invincibleTimerHandle, this, &UPlayerCharacterState::CheckInvincibleTime, 0.5f, true, 0.0f);
			invincibleTime = 1.5f; 
			invincible = true;
		}
	}
	auto PlayerController = GetWorld()->GetFirstPlayerController();
	auto PlayerCharacter = Cast<APlayerCharacterController>(PlayerController)->getPlayerCharacter();
	PlayerCharacter->getPlayerUI()->SetHP(playerMaxHp, playerCurHp);
}

void UPlayerCharacterState::setExtraState()
{
	if (playerExtraState != E_PlayerState::Player_NONE) {
		EPlayerState = playerExtraState;
	}
}

void UPlayerCharacterState::CheckInvincibleTime()
{
	invincibleTime -= 0.5f;
	if (invincibleTime <= 0.0f) {
		GetWorld()->GetTimerManager().ClearTimer(invincibleTimerHandle);
		invincible = false;
	}
}
