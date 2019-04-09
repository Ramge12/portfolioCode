#pragma once

#include "ZeldaBOTW.h"
#include "PlayerChracterMoveController.h"
#include "PlayerCharacterBattleController.h"
#include "GameFramework/PlayerController.h"
#include "PlayerCharacterController.generated.h"

UCLASS()
class ZELDABOTW_API APlayerCharacterController : public APlayerController
{
	GENERATED_BODY()

public:
	UPROPERTY(Category = Character, VisibleAnywhere, BlueprintReadOnly, meta = (AllowPrivateAccess = "true"))
	bool showInventoryUI;

private:
	UPROPERTY(Category = Character, VisibleAnywhere, BlueprintReadOnly, meta = (AllowPrivateAccess = "true"))
		class APlayerCharacter* PlayerCharacter;
	UPROPERTY(Category = Character, VisibleAnywhere, BlueprintReadOnly, meta = (AllowPrivateAccess = "true"))
		class UPlayerCharacterState* PlayerCharacterState;
	UPROPERTY(Category = Character, VisibleAnywhere, BlueprintReadOnly, meta = (AllowPrivateAccess = "true"))
		class UPlayerTrace* PlayerTrace;
	UPROPERTY(Category = Character, VisibleAnywhere, BlueprintReadOnly, meta = (AllowPrivateAccess = "true"))
		class APlayerWeapon* PlayerWeapon;
	UPROPERTY(Category = Character, VisibleAnywhere, BlueprintReadOnly, meta = (AllowPrivateAccess = "true"))
		APlayerChracterMoveController* MoveController;
	UPROPERTY(Category = Character, VisibleAnywhere, BlueprintReadOnly, meta = (AllowPrivateAccess = "true"))
		APlayerCharacterBattleController* BattleController;
	
protected:
	virtual void BeginPlay() override;

public:
	APlayerCharacterController();

	virtual void Tick(float DeltaTime) override;

	virtual void SetupInputComponent() override;
	virtual void Possess(APawn* aPawn) override;

	void KeyDownCheck();

	void ForwardBack(float NewAxisValue);
	void LeftRight(float NewAxisValue);

	void LookUpDown(float NewAxisValue);
	void Turn(float NewAxisValue);
	

	void ShowInventoryUI();
	void MouseOn();

	bool getShowInventoryUIValue() { return showInventoryUI; }
	class APlayerWeapon* getPlayerWeapon() { return PlayerWeapon; }
	class UPlayerCharacterState* getPlayerState() { return PlayerCharacterState; }
	class APlayerCharacter* getPlayerCharacter() { return PlayerCharacter; }
	APlayerChracterMoveController* getMoveController() { return MoveController; }
	APlayerCharacterBattleController* getBattleController() { return BattleController; }

	
};
