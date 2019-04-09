#include "PlayerCharacterBattleController.h"
#include "Player/PlayerCharacter.h"
#include "Player/PlayerCharacterState.h"
#include "Player/PlayerTrace.h"
#include "Player/Controller/PlayerCharacterController.h"

APlayerCharacterBattleController::APlayerCharacterBattleController()
{
	IsBattle = true;
	IsShoot = false;
	IsSword = false;
	CanShootArrow = false;
	WeaponIdleTime = 5;
}

void APlayerCharacterBattleController::PlayerBattleSetting(APlayerCharacter * Character, UPlayerCharacterState * Sate, UPlayerTrace* Trace)
{
	PlayerCharacter= Character;
	PlayerCharacterState= Sate;
	PlayerTrace = Trace;
}

void APlayerCharacterBattleController::PlayerShotStart()
{
	if (PlayerWeapon->checkParentComponent(E_Weapon::Weapon_BOW, TEXT("LeftHandSocket"))) {
		PlayerCharacterState->setPlayerState(E_PlayerState::Player_SHOT);
		PlayerCharacter->getPlayerCamera()->ChangeView(PlayerViewState::View_SHOT);
	}
	if (PlayerWeapon->checkParentComponent(E_Weapon::Weapon_SHIELD, TEXT("LeftArmGardSocket"))) {
		PlayerCharacterState->setPlayerState(E_PlayerState::Player_SHIELD);
	}
}

void APlayerCharacterBattleController::PlayerShotEnd()
{
	GetWorldSettings()->SetTimeDilation(1.0f);
	PlayerCharacterState->setPlayerState(E_PlayerState::Player_IDLE);
	IsShoot = false;
	PlayerCharacter->getPlayerCamera()->ChangeView(PlayerViewState::View_IDLE);
	PlayerCharacter->getPlayerUI()->SetPlayerAim(false);
	PlayerWeapon->ShowArrowLocationRotation(FVector::ZeroVector, FRotator::ZeroRotator, CanShootArrow);
}

void APlayerCharacterBattleController::PlayerAttackStart()
{
	if (IsValid(PlayerCharacterState)) {
		if (PlayerCharacterState->CheckCurState(E_PlayerState::Player_SHOT) && CanShootArrow) {
			ShootArrow();
			CanShootArrow = false;
			IsShoot = false;
			PlayerShotEnd();
			PlayerCharacter->getPlayerUI()->SetPlayerAim(false);
		}
		if (PlayerWeapon->checkParentComponent(E_Weapon::Weapon_SWORD, TEXT("RightHandSocket"))) {
			if (PlayerCharacterState->CheckCurState(E_PlayerState::Player_SHIELD)) {
				if (PlayerCharacterState->getCounterPossibility() && !PlayerCharacterState->getCounterAttackValue()) {
					PlayerCharacterState->setCounterPossibility(false);
					PlayerCharacterState->setCounterAttackValue(true);
				}
			}
			else {
				PlayerCharacterState->setPlayerState(E_PlayerState::Player_SWORD);
			}
		}
	}
}

void APlayerCharacterBattleController::PlayerHurt()
{
	if (!PlayerCharacterState->CheckCurState(E_PlayerState::Player_SHIELD)) {
		PlayerCharacterState->setPlayerCurHP(-1);
	}
}

void APlayerCharacterBattleController::ShootArrow()
{
	PlayerCharacter->getPlayerCamera()->CameraVector();
	FVector ArrowLocation = PlayerCharacter->getPlayerMeshComponent()->getRootMesh()->GetSocketLocation(TEXT("LeftHandSocket"));
	FRotator ArrowRotator = PlayerCharacter->getPlayerCamera()->GetForwardVector().Rotation() + FRotator(0.0f, -90.0f, 90.0f);;
	FVector ArrowForward = PlayerCharacter->getPlayerCamera()->GetForwardVector();
	PlayerWeapon->FireArrow(ArrowLocation, ArrowRotator, ArrowForward);
}

void APlayerCharacterBattleController::WeaponToIdleTimer()
{
	if (PlayerCharacterState->CheckCurState(E_PlayerState::Player_IDLE)) {
		WeaponIdleTime--;
	}
	else {
		WeaponIdleTime = 5;
	}
	if (WeaponIdleTime <= 0){
		GetWorldTimerManager().ClearTimer(AttackTimerHandle);
		WeaponChangeIDLE();
	}
}

void APlayerCharacterBattleController::WeaponToIdle()
{
	GetWorldTimerManager().ClearTimer(AttackTimerHandle);
	GetWorldTimerManager().SetTimer(AttackTimerHandle, this, &APlayerCharacterBattleController::WeaponToIdleTimer, 1.0f, true, 0.0f);
	WeaponIdleTime = 5;
}

void APlayerCharacterBattleController::WeaponChangeIDLE()
{
	IsBattle = false;
	CanShootArrow = false;
	IsShoot = false;
	IsSword = false;
	if (PlayerWeapon->checkParentComponent(E_Weapon::Weapon_BOW, TEXT("LeftHandSocket"))) {
		PlayerCharacterState->setPlayerState(E_PlayerState::Player_SHOT);
	}
	if (PlayerWeapon->checkParentComponent(E_Weapon::Weapon_SWORD, TEXT("RightHandSocket"))) {
		PlayerCharacterState->setPlayerState(E_PlayerState::Player_SWORD);
	}
}

void APlayerCharacterBattleController::WeaponChangeSword()
{
	WeaponToIdle();
	IsBattle = true;
	CanShootArrow = false;
	IsShoot = false;

	auto PlayerInvenroty = PlayerCharacter->getPlayerInvenroty();

	if (!PlayerWeapon->checkParentComponent(E_Weapon::Weapon_SWORD, TEXT("RightHandSocket")) && PlayerInvenroty->getWeapon_SwordList().Num() > 0) {
		PlayerWeapon->setWeaponComponent(E_Weapon::Weapon_SWORD, PlayerCharacter->getPlayerMeshComponent()->getRootMesh(), TEXT("RightHandSocket"));
		PlayerCharacterState->setPlayerState(E_PlayerState::Player_SWORD);
	}
	if (!PlayerWeapon->checkParentComponent(E_Weapon::Weapon_SHIELD, TEXT("LeftArmGardSocket")) && PlayerInvenroty->getWeapon_ShieldList().Num() > 0) {
		PlayerWeapon->setWeaponComponent(E_Weapon::Weapon_SHIELD, PlayerCharacter->getPlayerMeshComponent()->getRootMesh(), TEXT("LeftArmGardSocket"));
	}
}

void APlayerCharacterBattleController::WeaponChangeBow()
{
	auto PlayerInvenroty = PlayerCharacter->getPlayerInvenroty();

	IsBattle = true;
	WeaponToIdle();
	if (!PlayerWeapon->checkParentComponent(E_Weapon::Weapon_BOW, TEXT("LeftHandSocket")) && PlayerInvenroty->getWeapon_BowList().Num() > 0) {
		IsBattle = true;
		PlayerWeapon->setWeaponComponent(E_Weapon::Weapon_BOW, PlayerCharacter->getPlayerMeshComponent()->getRootMesh(), TEXT("LeftHandSocket"));
		if (IsValid(PlayerCharacterState)) {
			if (PlayerCharacterState->CheckCurState(E_PlayerState::Player_IDLE) ||
				PlayerCharacterState->CheckCurState(E_PlayerState::Player_WALK) ||
				PlayerCharacterState->CheckCurState(E_PlayerState::Player_RUN)) {
				PlayerCharacterState->setPlayerState(E_PlayerState::Player_SHOT);
			}
		}
	}
}

void APlayerCharacterBattleController::CounterAttackEnemy()
{
	PlayerTrace->PlayerCounterAttack(PlayerCharacter->GetActorLocation(), PlayerCharacter->GetActorForwardVector(), PlayerCharacter);
}

void APlayerCharacterBattleController::PlayerWeaponChangeLeft()
{
	if (PlayerWeapon->checkParentComponent(E_Weapon::Weapon_BOW, TEXT("LeftHandSocket"))){
		IsBattle = true;
		WeaponToIdle();
		IsShoot = false;
		PlayerWeapon->ChangeWeapon(E_Weapon::Weapon_BOW, false);
		PlayerWeapon->setWeaponComponent(E_Weapon::Weapon_BOW, PlayerCharacter->getPlayerMeshComponent()->getRootMesh(), TEXT("LeftHandSocket"));
		PlayerCharacterState->setPlayerState(E_PlayerState::Player_SHOT);
	}
	else if (PlayerWeapon->checkParentComponent(E_Weapon::Weapon_SWORD, TEXT("RightHandSocket"))){
		PlayerWeapon->ChangeWeapon(E_Weapon::Weapon_SWORD, false);
		PlayerWeapon->setWeaponComponent(E_Weapon::Weapon_SWORD, PlayerCharacter->getPlayerMeshComponent()->getRootMesh(), TEXT("RightHandSocket"));
		PlayerCharacterState->setPlayerState(E_PlayerState::Player_SWORD);
	}
}

void APlayerCharacterBattleController::PlayerWeaponChangeRight()
{
	if (PlayerWeapon->checkParentComponent(E_Weapon::Weapon_BOW, TEXT("LeftHandSocket"))) {
		IsBattle = true;
		WeaponToIdle();
		IsShoot = false;
		PlayerWeapon->ChangeWeapon(E_Weapon::Weapon_BOW, true);
		PlayerWeapon->setWeaponComponent(E_Weapon::Weapon_BOW, PlayerCharacter->getPlayerMeshComponent()->getRootMesh(), TEXT("LeftHandSocket"));
		PlayerCharacterState->setPlayerState(E_PlayerState::Player_SHOT);
	}
	else if (PlayerWeapon->checkParentComponent(E_Weapon::Weapon_SWORD, TEXT("RightHandSocket"))) {
		PlayerWeapon->ChangeWeapon(E_Weapon::Weapon_SWORD, true);
		PlayerWeapon->setWeaponComponent(E_Weapon::Weapon_SWORD, PlayerCharacter->getPlayerMeshComponent()->getRootMesh(), TEXT("RightHandSocket"));
		PlayerCharacterState->setPlayerState(E_PlayerState::Player_SWORD);
	}
}
