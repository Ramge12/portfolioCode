// Fill out your copyright notice in the Description page of Project Settings.

#include "BossCharacter.h"
#include "Enemy/AI/BossAIController.h"
#include "Runtime/AIModule/Classes/Perception/PawnSensingComponent.h"
#include "Player/Controller/PlayerCharacterController.h"
#include "Components/ProgressBar.h"
#include "Runtime/UMG/Public/IUMGModule.h"
#include "Components/WidgetComponent.h"
#include "Blueprint/UserWidget.h"

ABossCharacter::ABossCharacter()
{
 	PrimaryActorTick.bCanEverTick = true;
	BossHeadMesh = CreateDefaultSubobject<USkeletalMeshComponent>(TEXT("BossMesh"));
	BossState = CreateDefaultSubobject<UEnemyCharacterState>(TEXT("BossState"));

	BossHeadMesh->SetupAttachment(GetMesh());

	GetMesh()->SetAnimationMode(EAnimationMode::AnimationBlueprint);
	static ConstructorHelpers::FClassFinder<UAnimInstance> Boss_Anim(TEXT("/Game/EnemyCharacter/GuardianBoss/anim/BossAnim.BossAnim_C"));
	if (Boss_Anim.Succeeded()) {
		GetMesh()->SetAnimInstanceClass(Boss_Anim.Class);
	}
	patrolRange=1000.0f;
	FIndRange=100.0f;
	AttackRange=1000.0f;
	GetCharacterMovement()->MaxWalkSpeed = 200.0f;
	AIControllerClass = ABossAIController::StaticClass();
	AutoPossessAI = EAutoPossessAI::PlacedInWorldOrSpawned;
	OnBattle = false;

	static ConstructorHelpers::FClassFinder<UBossUIClass> UI_AIM(TEXT("/Game/UI/BossUI.BossUI_C"));
	if (UI_AIM.Succeeded()) {
		HDWidgetClass = UI_AIM.Class;
	}
}

void ABossCharacter::BeginPlay()
{
	Super::BeginPlay();
	BossUI = CreateWidget<UBossUIClass>(Cast< APlayerCharacterController>(GetWorld()->GetFirstPlayerController()), HDWidgetClass);
	BossUI->AddToViewport();
	BossUI->SetVisibility(ESlateVisibility::Hidden);
	BossState->setMaxHP(100.f);
}

void ABossCharacter::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);
	if (!OnBattle) {
		FRotator headRotator = FMath::RInterpTo(BossHeadMesh->RelativeRotation, GetMesh()->GetSocketRotation("HeadGear"), DeltaTime, 3.f);
		BossHeadMesh->SetRelativeRotation(headRotator);
	}
}

void ABossCharacter::setBossHPUI(bool value)
{
	if (value)
	{
		BossUI->SetVisibility(ESlateVisibility::Visible);
	}
	else {
		BossUI->SetVisibility(ESlateVisibility::Hidden);
	}
}

void ABossCharacter::HurtBoss()
{
	BossState->HurtEnemy();
	BossUI->SetHP(BossState->getCurHp(), BossState->getMaxHp());
	BossState->setCurEnemyState(EnemyState::Enemy_IDLE);
}

