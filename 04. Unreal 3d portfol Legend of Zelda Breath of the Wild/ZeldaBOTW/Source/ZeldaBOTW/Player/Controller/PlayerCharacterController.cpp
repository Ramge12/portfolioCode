// Fill out your copyright notice in the Description page of Project Settings.

#include "PlayerCharacterController.h"
#include "Player/PlayerCamera.h"
#include "DrawDebugHelpers.h"
#include "Player/PlayerCharacter.h"
#include "Player/PlayerCharacterState.h"
#include "Player/PlayerTrace.h"
#include "Player/PlayerWeapon.h"
#include "Weapon/Weapon_Arrow.h"

APlayerCharacterController::APlayerCharacterController()
{
	showInventoryUI = false;
	PlayerTrace = CreateDefaultSubobject<UPlayerTrace>(TEXT("PlayerTrace"));
	PlayerWeapon = CreateDefaultSubobject<APlayerWeapon>(TEXT("PlayerWeapon"));
	PlayerCharacterState = CreateDefaultSubobject<UPlayerCharacterState>(TEXT("p2PlayerSate"));
	MoveController = CreateDefaultSubobject<APlayerChracterMoveController>(TEXT("MoveController"));
	BattleController = CreateDefaultSubobject<APlayerCharacterBattleController>(TEXT("BattleController"));
}

void APlayerCharacterController::BeginPlay()
{
	Super::BeginPlay();
	PlayerWeapon = GetWorld()->SpawnActor<APlayerWeapon>(FVector::ZeroVector, FRotator::ZeroRotator);
	BattleController->setPlayerWeapon(PlayerWeapon);
}

void APlayerCharacterController::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);
	MoveController->Tick(DeltaTime);
	BattleController->Tick(DeltaTime);
	KeyDownCheck();
	PlayerCharacter->getPlayerUI()->setStaminaVlaue((float)PlayerCharacterState->getStaminaPercent());
	
}

void APlayerCharacterController::SetupInputComponent()
{
	Super::SetupInputComponent();
	InputComponent->BindAxis(TEXT("MoveForwardBack"), this, &APlayerCharacterController::ForwardBack);
	InputComponent->BindAxis(TEXT("MoveLeftRight"), this, &APlayerCharacterController::LeftRight);
	InputComponent->BindAxis(TEXT("Turn"), this, &APlayerCharacterController::Turn);
	InputComponent->BindAxis(TEXT("LookUpDown"), this, &APlayerCharacterController::LookUpDown);

	InputComponent->BindAction(TEXT("Jump"), EInputEvent::IE_Pressed, MoveController, &APlayerChracterMoveController::PlayerJump);
	InputComponent->BindAction(TEXT("Run"), EInputEvent::IE_Pressed, MoveController, &APlayerChracterMoveController::PlayerRunStart);
	InputComponent->BindAction(TEXT("Run"), EInputEvent::IE_Released, MoveController, &APlayerChracterMoveController::PlayerRunStop);
	InputComponent->BindAction(TEXT("Crouch"), EInputEvent::IE_Pressed, MoveController, &APlayerChracterMoveController::PlayerCrouch);
	InputComponent->BindAction(TEXT("Crouch"), EInputEvent::IE_Released, MoveController, &APlayerChracterMoveController::PlayerUnCrouch);

	InputComponent->BindAction(TEXT("ExtraInput"), EInputEvent::IE_Pressed, MoveController, &APlayerChracterMoveController::PlayerExtraState);

	InputComponent->BindAction(TEXT("Shot"), EInputEvent::IE_Pressed, BattleController, &APlayerCharacterBattleController::PlayerShotStart);
	InputComponent->BindAction(TEXT("Shot"), EInputEvent::IE_Released, BattleController, &APlayerCharacterBattleController::PlayerShotEnd);
	InputComponent->BindAction(TEXT("Attack"), EInputEvent::IE_Pressed, BattleController, &APlayerCharacterBattleController::PlayerAttackStart);
	
	InputComponent->BindAction(TEXT("Inventory"), EInputEvent::IE_Pressed, this, &APlayerCharacterController::ShowInventoryUI);

	InputComponent->BindAction(TEXT("WeaponChange1"), EInputEvent::IE_Pressed, BattleController, &APlayerCharacterBattleController::WeaponChangeIDLE);
	InputComponent->BindAction(TEXT("WeaponChange2"), EInputEvent::IE_Pressed, BattleController, &APlayerCharacterBattleController::WeaponChangeSword);
	InputComponent->BindAction(TEXT("WeaponChange3"), EInputEvent::IE_Pressed, BattleController, &APlayerCharacterBattleController::WeaponChangeBow);

	InputComponent->BindAction(TEXT("WeaponChangeLeft"), EInputEvent::IE_Pressed, BattleController, &APlayerCharacterBattleController::PlayerWeaponChangeLeft);
	InputComponent->BindAction(TEXT("WeaponChangeRight"), EInputEvent::IE_Pressed, BattleController, &APlayerCharacterBattleController::PlayerWeaponChangeRight);
}

void APlayerCharacterController::Possess(APawn * aPawn)
{
	Super::Possess(aPawn);
	if (aPawn) {
		PlayerCharacter = Cast< APlayerCharacter>(aPawn);
		MoveController->PlayerMoveSetting(PlayerCharacter, PlayerCharacterState, PlayerTrace);
		BattleController->PlayerBattleSetting(PlayerCharacter, PlayerCharacterState, PlayerTrace);
	}
}

void APlayerCharacterController::KeyDownCheck()
{
	if (!IsInputKeyDown(EKeys::W) && !IsInputKeyDown(EKeys::S) && !IsInputKeyDown(EKeys::A) && !IsInputKeyDown(EKeys::D)) {
		MoveController->KeyDownCheck();
	}
}

void APlayerCharacterController::ForwardBack(float NewAxisValue)
{
	if (!PlayerCharacter->getPlayerUI()->getInvenrotyActive()) {
		if (PlayerCharacter->GetMovementComponent()->IsMovingOnGround()) {
			PlayerCharacterState->CheckState_and_setState(E_PlayerState::Player_IDLE, E_PlayerState::Player_WALK);
		}
		if (PlayerCharacterState->CheckCurState(E_PlayerState::Player_CLIMING)) {
			PlayerCharacter->SetActorLocation(PlayerCharacter->GetActorLocation() + PlayerCharacter->GetActorUpVector()*MoveController->getCurSpeed()*NewAxisValue*0.5f);
		}
		else if (!PlayerCharacterState->CheckCurState(E_PlayerState::Player_SHOT)) {
			if (!PlayerCharacterState->CheckCurState(E_PlayerState::Player_SHOT) && !PlayerCharacterState->CheckCurState(E_PlayerState::Player_SHIELD) && !PlayerCharacterState->CheckCurState(E_PlayerState::Player_SWORD) && !PlayerCharacterState->CheckCurState(E_PlayerState::Player_HURT) && !PlayerCharacterState->CheckCurState(E_PlayerState::Player_DEATH)) {
				PlayerCharacter->AddMovementInput(FRotationMatrix(GetControlRotation()).GetUnitAxis(EAxis::X), NewAxisValue * MoveController->getCurSpeed());
			}
		}
		else if (!PlayerCharacterState->CheckCurState(E_PlayerState::Player_SWORD) && !PlayerCharacterState->CheckCurState(E_PlayerState::Player_HURT) && !PlayerCharacterState->CheckCurState(E_PlayerState::Player_DEATH)) {
			PlayerCharacter->SetActorLocation(PlayerCharacter->GetActorLocation() + PlayerCharacter->GetActorForwardVector()*MoveController->getCurSpeed()*NewAxisValue*0.5f);
		}
		MoveController->setIsMoveValue(true);
	}
}

void APlayerCharacterController::LeftRight(float NewAxisValue)
{
	if (!PlayerCharacter->getPlayerUI()->getInvenrotyActive()) {
		if (PlayerCharacter->GetMovementComponent()->IsMovingOnGround()) {
			PlayerCharacterState->CheckState_and_setState(E_PlayerState::Player_IDLE, E_PlayerState::Player_WALK);
		}
		if (PlayerCharacterState->CheckCurState(E_PlayerState::Player_CLIMING)) {
			if (MoveController->getIsMove())PlayerCharacter->SetActorLocation(PlayerCharacter->GetActorLocation() + PlayerCharacter->GetActorRightVector()*MoveController->getCurSpeed()*NewAxisValue);
		}
		else if (!PlayerCharacterState->CheckCurState(E_PlayerState::Player_SHOT)) {
			if (!PlayerCharacterState->CheckCurState(E_PlayerState::Player_SHOT) && !PlayerCharacterState->CheckCurState(E_PlayerState::Player_SHIELD) && !PlayerCharacterState->CheckCurState(E_PlayerState::Player_SWORD) && !PlayerCharacterState->CheckCurState(E_PlayerState::Player_HURT) && !PlayerCharacterState->CheckCurState(E_PlayerState::Player_DEATH)) {
				PlayerCharacter->AddMovementInput(FRotationMatrix(GetControlRotation()).GetUnitAxis(EAxis::Y), NewAxisValue * MoveController->getCurSpeed());
			}
		}
		else if (!PlayerCharacterState->CheckCurState(E_PlayerState::Player_SWORD) && !PlayerCharacterState->CheckCurState(E_PlayerState::Player_HURT) && !PlayerCharacterState->CheckCurState(E_PlayerState::Player_DEATH)) {
			PlayerCharacter->SetActorLocation(PlayerCharacter->GetActorLocation() + PlayerCharacter->GetActorRightVector()*MoveController->getCurSpeed()*NewAxisValue*0.5f);
		}
	}
}

void APlayerCharacterController::LookUpDown(float NewAxisValue)
{
	float LookPitch = GetControlRotation().Pitch;
	float PitchMin = 330.0f;
	float PitchMax = 5.0f;
	if (!PlayerCharacter->getPlayerUI()->getInvenrotyActive()) {
		if (!PlayerCharacterState->CheckCurState(E_PlayerState::Player_SHOT)) {
			if (LookPitch > PitchMin || LookPitch < PitchMax) {
				LookPitch += NewAxisValue;
				if (LookPitch > PitchMin || LookPitch < PitchMax) {
					SetControlRotation(FRotator(LookPitch, GetControlRotation().Yaw, GetControlRotation().Roll));
				}
			}
			else if (LookPitch <= PitchMin) {
				LookPitch = PitchMin + NewAxisValue;
				SetControlRotation(FRotator(LookPitch, GetControlRotation().Yaw, GetControlRotation().Roll));
			}
			else if (LookPitch >= PitchMax) {
				LookPitch = PitchMax - NewAxisValue;
				SetControlRotation(FRotator(LookPitch, GetControlRotation().Yaw, GetControlRotation().Roll));
			}
		}
		else {
			PlayerCharacter->AddControllerPitchInput(-NewAxisValue);
		}
	}
}

void APlayerCharacterController::Turn(float NewAxisValue)
{
	static bool shotCheck = false;
	static float rotationValue = 0.0f;
	if (!PlayerCharacter->getPlayerUI()->getInvenrotyActive() && IsValid(PlayerCharacterState)) {
		if (PlayerCharacterState->CheckCurState(E_PlayerState::Player_SHOT) && (PlayerCharacter->getPlayerCamera()->getPlayerViewState() == PlayerViewState::View_SHOT)) {
			static float LookYaw = 0.0f;
			if (!shotCheck) {
				rotationValue = GetControlRotation().Yaw;
				PlayerCharacter->SetActorRotation(FRotator(PlayerCharacter->GetActorRotation().Pitch, GetControlRotation().Yaw, PlayerCharacter->GetActorRotation().Roll));
				LookYaw = GetControlRotation().Yaw;
				shotCheck = true;
			}
			PlayerWeapon->ShowArrowLocationRotation(PlayerCharacter->getPlayerMeshComponent()->getRootMesh()->GetSocketLocation(TEXT("LeftHandSocket")), GetControlRotation(), BattleController->getCanShootArrow());
			float YawMin = rotationValue - 45.0f;
			float YawMax = rotationValue + 45.0f;
			if (LookYaw >= YawMin && LookYaw <= YawMax) {
				LookYaw += NewAxisValue;
				if (LookYaw >= YawMin && LookYaw <= YawMax) {
					SetControlRotation(FRotator(GetControlRotation().Pitch, LookYaw, GetControlRotation().Roll));
				}
			}
			else if (LookYaw <= YawMin) {
				LookYaw = YawMin + NewAxisValue;
				SetControlRotation(FRotator(GetControlRotation().Pitch, LookYaw, GetControlRotation().Roll));
			}
			else if (LookYaw >= YawMax) {
				LookYaw = YawMax - NewAxisValue;
				SetControlRotation(FRotator(GetControlRotation().Pitch, LookYaw, GetControlRotation().Roll));
			}
		}
		else if (PlayerCharacterState->CheckCurState(E_PlayerState::Player_SHIELD)) {
			PlayerCharacter->SetActorRotation(FRotator(PlayerCharacter->GetActorRotation().Pitch, GetControlRotation().Yaw, PlayerCharacter->GetActorRotation().Roll));
		}
		else {
			shotCheck = false;
			PlayerCharacter->AddControllerYawInput(NewAxisValue);
		}
	}
}

void APlayerCharacterController::ShowInventoryUI()
{
	PlayerCharacter->getPlayerUI()->ShowInventory();
	if (showInventoryUI)showInventoryUI = false;
	else showInventoryUI = true;
}

