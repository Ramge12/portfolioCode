// Fill out your copyright notice in the Description page of Project Settings.

#include "NPC_Shop.h"
#include "Player/Controller/PlayerCharacterController.h"
#include "Player/PlayerCharacter.h"
#include "Player/PlayerUIWidget.h"
#include "Components/ProgressBar.h"
#include "Runtime/UMG/Public/IUMGModule.h"
#include "Components/WidgetComponent.h"
#include "Blueprint/UserWidget.h"

// Sets default values
ANPC_Shop::ANPC_Shop()
{
	// Set this character to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;
	NPCMesh = CreateDefaultSubobject<USkeletalMeshComponent>(TEXT("NPCMesh"));
	DetectMark = CreateDefaultSubobject<UWidgetComponent>(TEXT("HPBarWidget"));
	totalitemClass = CreateDefaultSubobject<UTotalItemClass>(TEXT("totalitemClass"));

	ExtraPlayerState = E_PlayerState::Player_SHOPPING;
	GetMesh()->SetAnimationMode(EAnimationMode::AnimationBlueprint);
	DetectMark->SetupAttachment(GetMesh());
	DetectMark->SetWidgetSpace(EWidgetSpace::World);

	static ConstructorHelpers::FClassFinder<UNPC_UI> NpcShopUI(TEXT("/Game/UI/NpcShopUI.NpcShopUI_C"));
	if (NpcShopUI.Succeeded()) {
		HDWidgetClass = NpcShopUI.Class;
	}

	static ConstructorHelpers::FClassFinder<UUserWidget> m_UI(TEXT("/Game/UI/NPC_UI.NPC_UI_C"));
	if (m_UI.Succeeded()) {
		DetectMark->SetWidgetClass(m_UI.Class);
		DetectMark->SetDrawSize(FVector2D(100.f, 100.f));
	}
	DetectMark->SetRelativeLocation(FVector(-20.f, 0, 130.0f));
	static ConstructorHelpers::FClassFinder<UAnimInstance> NPC_ANIM(TEXT("/Game/Character/NPC/Animation/NPCANIMATION.NPCANIMATION_C"));
	if (NPC_ANIM.Succeeded()) {
		GetMesh()->SetAnimInstanceClass(NPC_ANIM.Class);
	}
	ShopRange = 150.0f;
}

void ANPC_Shop::BeginPlay()
{
	Super::BeginPlay();
	NPC_UI = CreateWidget<UNPC_UI>(Cast< APlayerCharacterController>(GetWorld()->GetFirstPlayerController()), HDWidgetClass);
	NPC_UI->AddToViewport(1);
	NPC_UI->SetVisibility(ESlateVisibility::Hidden);
	NPC_UI->setNpcShop(this);
}

// Called every frame
void ANPC_Shop::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);
	auto PlayerController = GetWorld()->GetFirstPlayerController();
	auto PController = Cast< APlayerCharacterController>(PlayerController);
	float distanceToPlayer = FVector::Dist(GetActorLocation(), PController->getPlayerCharacter()->GetActorLocation());

	if (distanceToPlayer < ShopRange) {
		if (PController->getPlayerState()->getPlayerExtraState() == E_PlayerState::Player_SHOPPING) {
			DetectMark->SetVisibility(true);

		}
		if(PController->getPlayerState()->getCurPlayerState() == E_PlayerState::Player_SHOPPING && !PController->getPlayerCharacter()->getPlayerUI()->getShopUI()){
			NPC_UI->SetVisibility(ESlateVisibility::Visible);
			NPC_UI->setNpcShop(this);

		}
		else if(!PController->getShowInventoryUIValue()){
			NPC_UI->SetVisibility(ESlateVisibility::Hidden);
		}
	}
	else {
		DetectMark->SetVisibility(false);
		NPC_UI->SetVisibility(ESlateVisibility::Hidden);
	}
	if (::IsValid(PlayerController)) {
		if (PController->getPlayerCharacter()) {
			FVector Xrotator = (DetectMark->GetComponentLocation() - PController->getPlayerCharacter()->getPlayerCamera()->GetComponentLocation());
			DetectMark->SetWorldRotation(FRotator(0, Xrotator.Rotation().Yaw,0) + FRotator(0, 180.0f, 0));
		}
	}
}


