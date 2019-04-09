#pragma once

#include "ZeldaBOTW.h"
#include "Components/ActorComponent.h"
#include "PlayerCharacterState.generated.h"

UENUM()
enum class E_PlayerState : uint8 {
	Player_NONE		UMETA(DisplayName = "Player_NONE"),
	Player_IDLE		UMETA(DisplayName = "Player_IDLE"),
	Player_WALK		UMETA(DisplayName = "Player_WALK"),
	Player_JUMP		UMETA(DisplayName = "Player_JUMP"),
	Player_RUN		UMETA(DisplayName = "Player_RUN"),
	Player_COLLECT  UMETA(DisplayName = "Player_COLLECT"),
	Player_CROUCH	UMETA(DisplayName = "Player_CROUCH"),
	Player_PUSH		UMETA(DisplayName = "Player_PUSH"),
	Player_CLIMING	UMETA(DisplayName = "Player_CLIMING"),
	Player_SHOT		UMETA(DisplayName = "Player_SHOT"),
	Player_SWORD	UMETA(DisplayName = "Player_SWORD"),
	Player_SHIELD	UMETA(DisplayName = "Player_SHIELD"),
	Player_HURT		UMETA(DisplayName = "Player_HURT"),
	Player_DEATH	UMETA(DisplayName = "Player_DEATH"),
	Player_Carry	UMETA(DisplayName = "Player_Carry"),
	Player_SHOPPING	UMETA(DisplayName = "Player_Shopping")
};

UCLASS( ClassGroup=(Custom), meta=(BlueprintSpawnableComponent) )
class ZELDABOTW_API UPlayerCharacterState : public UActorComponent
{
	GENERATED_BODY()
private:
	int32 playerATK;
	int32 playerCurHp;
	int32 playerMaxHp;
	bool invincible;
	bool StaminaUse;
	bool StaminCharge;
	float staminValue;
	float invincibleTime;

	FTimerHandle invincibleTimerHandle;
private:
	UPROPERTY(Category = State, VisibleAnywhere, BlueprintReadOnly, meta = (AllowPrivateAccess = "true"))
		E_PlayerState EPlayerState;
	UPROPERTY(Category = State, VisibleAnywhere, BlueprintReadOnly, meta = (AllowPrivateAccess = "true"))
		E_PlayerState preEPlayerState;
	UPROPERTY(Category = State, VisibleAnywhere, BlueprintReadOnly, meta = (AllowPrivateAccess = "true"))
		E_PlayerState playerExtraState;
	UPROPERTY(Category = State, VisibleAnywhere, BlueprintReadOnly, meta = (AllowPrivateAccess = "true"))
		bool ThrowObject;
	UPROPERTY(Category = State, VisibleAnywhere, BlueprintReadOnly, meta = (AllowPrivateAccess = "true"))
		bool CounterAttack;
	UPROPERTY(Category = State, VisibleAnywhere, BlueprintReadOnly, meta = (AllowPrivateAccess = "true"))
		bool CounterPossibility;
public:
	UPlayerCharacterState();

	virtual void TickComponent(float DeltaTime, ELevelTick TickType, FActorComponentTickFunction* ThisTickFunction) override;

	void setPlayerState(E_PlayerState setValue);
	bool CheckCurState(E_PlayerState setValue);
	void CheckState_and_setState(E_PlayerState isCheckValue, E_PlayerState setValue);

	void setPlayerCurHP(int32 value);

	void setExtraState();
	void CheckInvincibleTime();
	void setMaxHP(int32 value) { playerMaxHp = value; }

	void setThrowObject(bool value) { ThrowObject = value; }

	void setExtraState(E_PlayerState Extra) { playerExtraState = Extra; }
	void setCounterAttackValue(bool value) { CounterAttack = value; }
	void setCounterPossibility(bool value) { CounterPossibility = value; }
	void getStaminaUseSet(bool value) { StaminaUse = value; }
	void StaminaSetting(float value) { staminValue += value; }
	void setStaminaCharge(bool value) { StaminCharge = value; }

	E_PlayerState getCurPlayerState() { return EPlayerState; }
	E_PlayerState getPrePlayerState() { return preEPlayerState; }
	E_PlayerState getPlayerExtraState() { return playerExtraState; }
	int32 getPlayerCurHp() { return playerCurHp; }
	int32 getPlayerMaxHp() { return playerMaxHp; }
	float getStaminaPercent() { return staminValue; }
	bool getStaminaCharge() { return StaminCharge; }
	bool getThrowValue() { return ThrowObject; }
	bool getCounterAttackValue() { return CounterAttack; }
	bool getCounterPossibility() { return CounterPossibility; }
};
