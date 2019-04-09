// Fill out your copyright notice in the Description page of Project Settings.

#include "EnemyCharacter.h"
#include "Enemy/AI/EnemyAIController.h"
#include "Player/Controller/PlayerCharacterController.h"
#include "Runtime/AIModule/Classes/Perception/PawnSensingComponent.h"
#include "Components/WidgetComponent.h"
#include "Components/ProgressBar.h"
#include "Blueprint/UserWidget.h"
#include "Runtime/UMG/Public/IUMGModule.h"
#include "Weapon/Weapon_Arrow.h"
#include "Weapon/Weapon_Equip.h"

// Sets default values
AEnemyCharacter::AEnemyCharacter()
{
 	PrimaryActorTick.bCanEverTick = true;
	EnemyMesh = CreateDefaultSubobject<USkeletalMeshComponent>(TEXT("EnemyMesh"));
	EnemyState = CreateDefaultSubobject<UEnemyCharacterState>(TEXT("EnemyState"));
	pawnSensing = CreateDefaultSubobject<UPawnSensingComponent>(TEXT("pawnSensing"));
	DetectMark = CreateDefaultSubobject<UWidgetComponent>(TEXT("HPBarWidget"));
	WeaponLocation = CreateDefaultSubobject<AActor>(TEXT("WeaponLocation"));
	PlayerCharacterPawn = CreateDefaultSubobject<ACharacter>(TEXT("PlayerCharacter"));

	ConstructorHelpers::FObjectFinder<USkeletalMesh> m_Skeletal2(TEXT("/Game/EnemyCharacter/Bokoblin/Bokobline.Bokobline"));
	if (m_Skeletal2.Succeeded()) {
		EnemyMesh->SetSkeletalMesh(m_Skeletal2.Object);
	}

	EnemyMesh->SetupAttachment(GetMesh());
	DetectMark->SetupAttachment(GetMesh());
	DetectMark->SetRelativeLocation(FVector(0.0f, 20.0f, 120.0f));
	DetectMark->SetWidgetSpace(EWidgetSpace::World);
	static ConstructorHelpers::FClassFinder<UUserWidget> m_UI(TEXT("/Game/UI/EnemyUI.EnemyUI_C"));
	if (m_UI.Succeeded()) {
		DetectMark->SetWidgetClass(m_UI.Class);
		DetectMark->SetDrawSize(FVector2D(20.f, 40.f));
	}
	DetectMark->SetRelativeLocation(FVector(0, 0, 110.0f));
	GetCapsuleComponent()->SetCapsuleRadius(30);
	GetCapsuleComponent()->SetCapsuleHalfHeight(50);
	GetMesh()->SetRelativeLocationAndRotation(FVector(0.0f, 0.0f, -50.0f), FRotator(0.0f, -90.0f, 0.0f));

	EnemyMesh->SetAnimationMode(EAnimationMode::AnimationBlueprint);
	static ConstructorHelpers::FClassFinder<UAnimInstance> Player_ANIM(TEXT("/Game/EnemyCharacter/AnimInstance/EnemyAnim.EnemyAnim_C"));
	if (Player_ANIM.Succeeded()) {
		EnemyMesh->SetAnimInstanceClass(Player_ANIM.Class);
	}

	AIControllerClass = AEnemyAIController::StaticClass();
	AutoPossessAI = EAutoPossessAI::PlacedInWorldOrSpawned;

	bUseControllerRotationYaw = false;
	GetCharacterMovement()->bUseControllerDesiredRotation = false;
	GetCharacterMovement()->bOrientRotationToMovement = true;
	GetCharacterMovement()->RotationRate = FRotator(0.0f, 480.0f, 0.0f);

	GetCharacterMovement()->MaxWalkSpeed = 100.0f;
	EnemyChracterMaxSpeed = 100.0f;

	pawnSensing->OnSeePawn.AddDynamic(this, &AEnemyCharacter::OnPawnSeen);
	pawnSensing->SetPeripheralVisionAngle(35.0f);
	AttackTimer = 10.0f;
	GetCapsuleComponent()->ComponentTags.Add(FName("Enemy"));
	Weapon_Arrows = AWeapon_Arrow::StaticClass();
}

void AEnemyCharacter::BeginPlay()
{
	Super::BeginPlay();
	GetCharacterMovement()->MaxWalkSpeed = EnemyChracterMaxSpeed;
	GetCapsuleComponent()->OnComponentBeginOverlap.AddDynamic(this, &AEnemyCharacter::getWeapon);
}

void AEnemyCharacter::getWeapon(UPrimitiveComponent * OverlappedComponent, AActor * OtherActor, UPrimitiveComponent * OtherComp, int32 OtherBodyIndex, bool bFromSweep, const FHitResult & SweepResult)
{
	if (OtherComp->ComponentHasTag("Weapon") && !EnemyState->getHasWeapon()) {
		auto EquapWeapon2 = Cast<AWeapon_Equip>(OtherActor);
		if (EquapWeapon2 != nullptr) {
			if (EnemyState->getEnemyCharacterKind() == EnemyWeaponType::Weapon_Sword) {
				EquapWeapon2->setCharacterEquip(EnemyMesh, TEXT("RightHandSocket"));
			}
			else if (EnemyState->getEnemyCharacterKind() == EnemyWeaponType::Weapon_Bow) {
				EquapWeapon2->setCharacterEquip(EnemyMesh, TEXT("LeftHandSocket"));
			}
			EnemyState->setHasWeapon(true);
		}
	}
}

void AEnemyCharacter::OnPawnSeen(APawn * SeenPawn)
{
	if (SeenPawn != nullptr) {
		APlayerCharacter* PlayerCharacter = Cast< APlayerCharacter>(SeenPawn);
		if (PlayerCharacter != nullptr) {
			EnemyState->setOnBattle(true);
			PlayerCharacterPawn = SeenPawn;
			GetWorldTimerManager().ClearTimer(AttackTimerHandle);
			GetWorldTimerManager().SetTimer(AttackTimerHandle, this, &AEnemyCharacter::BattleToIdle, 1.0f, true, 0.0f);
			AttackTimer = 10.0f;
			return;
		}
	}
}

void AEnemyCharacter::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);
	DetectMark->GetComponentLocation();
	auto PlayerController = GetWorld()->GetFirstPlayerController();
	if (::IsValid(PlayerController)) {
		auto PController = Cast< APlayerCharacterController>(PlayerController);
		if (PController->getPlayerCharacter()) {
			FVector Xrotator = (DetectMark->GetComponentLocation() - PController->getPlayerCharacter()->getPlayerCamera()->GetComponentLocation());
			DetectMark->SetWorldRotation(Xrotator.Rotation() + FRotator(0, 180.0f, 0));
		}
	}
	if (EnemyState->getOnBattle()) {
		DetectMark->SetVisibility(true);
		GetCharacterMovement()->MaxWalkSpeed = 150.0f;
	}
	else {
		DetectMark->SetVisibility(false);
		GetCharacterMovement()->MaxWalkSpeed = 100.0f;
	}
	if (EnemyState->getCurEnemyState() == EnemyState::Enemy_DEATH) {
		GetCharacterMovement()->MaxWalkSpeed = 0.0f;
	}

}

void AEnemyCharacter::BattleToIdle()
{
	if (AttackTimer-- <= 0) {
		GetWorldTimerManager().ClearTimer(AttackTimerHandle);
		EnemyState->setOnBattle(false);
	}
}

void AEnemyCharacter::Hurt()
{
	EnemyState->HurtEnemy();
	auto EnemyHpBar = Cast<UProgressBar>(DetectMark->GetUserWidgetObject()->GetWidgetFromName(TEXT("HpBar")));
	if (IsValid(EnemyHpBar)) {
		EnemyHpBar->SetPercent(EnemyState->getCurHp() / EnemyState->getMaxHp());
		if (EnemyState->getCurHp() <= 0.0f) {
			DetectMark->SetVisibility(false);
		}
	}
}

void AEnemyCharacter::ShootArrow()
{
	FVector locatNewVelocityion = PlayerCharacterPawn->GetActorLocation() - GetActorLocation();
	locatNewVelocityion.Normalize();
	locatNewVelocityion *= 1000.0f;
	auto Arrow = GetWorld()->SpawnActor<AWeapon_Arrow>(Weapon_Arrows, GetActorLocation(), locatNewVelocityion.ToOrientationRotator());
	if (Arrow) {
		FVector NewVelocity = GetActorForwardVector() * 1000.0f;
		Arrow->setVelocity(locatNewVelocityion);
		Arrow->setHitPlayer(true);
	}
}

FVector AEnemyCharacter::getWeaponLocation()
{
	return WeaponLocation->GetActorLocation();
}

