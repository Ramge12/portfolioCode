// Fill out your copyright notice in the Description page of Project Settings.

#include "PlayerCamera.h"
#include "DrawDebugHelpers.h"
#include "PaperSpriteComponent.h"
#include "PaperSprite.h"
#include "Player/PlayerCameraShake.h"
#include "Player/Controller/PlayerCharacterController.h"
#include "GameFramework/SpringArmComponent.h"
#include "Components/WidgetComponent.h"
#include "Components/SceneCaptureComponent2D.h"
#include "Runtime/CoreUObject/Public/UObject/ConstructorHelpers.h"
#include "Runtime/Engine/Classes/Engine/TextureRenderTarget2D.h"

UPlayerCamera::UPlayerCamera()
{
	PrimaryComponentTick.bCanEverTick = true;
	SpringArm = CreateDefaultSubobject<USpringArmComponent>(TEXT("SPRINGARM"));
	PlayerUpperSpringArm = CreateDefaultSubobject<USpringArmComponent>(TEXT("PlayerUpperSpringArm"));
	playerCaputre= CreateDefaultSubobject<USceneCaptureComponent2D>(TEXT("playerCaputre"));
	RenderTarget = CreateDefaultSubobject<UTextureRenderTarget2D>(TEXT("RenderTarget"));
	paperSprite = CreateDefaultSubobject<UPaperSpriteComponent>(TEXT("paperSprite"));
	cameraShake = UPlayerCameraShake::StaticClass();

	this->SetupAttachment(SpringArm);
	SpringArm->TargetArmLength = 150.0f;
	ArmLengthTo = SpringArm->TargetArmLength;
	SpringArm->SetRelativeRotation(FRotator(-30.0f, 0.0f, 0.0f));
	SpringArm->bUsePawnControlRotation = true;
	SpringArm->bInheritPitch = true;
	SpringArm->bInheritRoll = true;
	SpringArm->bInheritYaw = true;
	SpringArm->bDoCollisionTest = false;
	ViewState = PlayerViewState::View_IDLE;
	ArmRotationSpeed = 5.0f;
	
	playerCaputre->SetupAttachment(PlayerUpperSpringArm);
	PlayerUpperSpringArm->TargetArmLength =1000.0f;
	UpperArmLengthTo = PlayerUpperSpringArm->TargetArmLength;
	PlayerUpperSpringArm->bDoCollisionTest = false;
	PlayerUpperSpringArm->SetRelativeRotation(FRotator(-90.0f, 0.0f, 0.0f));
	
	ConstructorHelpers::FObjectFinder<UTextureRenderTarget2D> m_Texture(TEXT("/Game/UI/RenderTarget/MinMapRenderTarget.MinMapRenderTarget"));
	if (m_Texture.Succeeded()) {
		playerCaputre->TextureTarget = m_Texture.Object;
	}

	ConstructorHelpers::FObjectFinder<UPaperSprite> m_Paper(TEXT("/Game/UI/RenderTarget/Cursor.Cursor"));
	if (m_Paper.Succeeded()) {
		paperSprite->SetRelativeRotation(FRotator(0,115, -90));
		paperSprite->SetRelativeScale3D(FVector(0.05f, 0.05f, 0.05f));
		paperSprite->SetSprite(m_Paper.Object);
		paperSprite->bOwnerNoSee = true;
		paperSprite->SetCollisionProfileName(TEXT("NoCollision"));
	}
}

void UPlayerCamera::setSpringArmCameraInRoot(UCapsuleComponent * root)
{
	SpringArm->SetupAttachment(root);
	paperSprite->SetupAttachment(root);
}

void UPlayerCamera::ChangeView(PlayerViewState view)
{
	switch (view) {
		case PlayerViewState::View_IDLE:
			ArmLengthTo = 150.0f;
			ViewState = PlayerViewState::View_IDLE;
			SpringArm->SetRelativeRotation(FRotator(-30.0f, 0.0f, 0.0f));
			SpringArm->SetRelativeLocation(FVector(0.0f, 0.0f, 0.0f));
			break;
		case PlayerViewState::View_SHOT:
			ArmLengthTo = 30.0f;
			ViewState = PlayerViewState::View_SHOT;
			SpringArm->SetRelativeRotation(FRotator(0.0f, 0.0f, 0.0f));
			SpringArm->SetRelativeLocation(FVector(37.5f, 0.0f, 30.0f));
			break;
		case PlayerViewState::View_SHIELD:
			ArmLengthTo = 110.0f;
			ViewState = PlayerViewState::View_SHOT;
			SpringArm->SetRelativeRotation(FRotator(0.0f, 0.0f, 0.0f));
			SpringArm->SetRelativeLocation(FVector(26.0f, 0.0f, 43.0f));
			break;
	}
}

void UPlayerCamera::TickComponent(float DeltaTime, ELevelTick TickType, FActorComponentTickFunction * ThisTickFunction)
{
	Super::TickComponent(DeltaTime, TickType, ThisTickFunction);
	SpringArm->TargetArmLength = FMath::FInterpTo(SpringArm->TargetArmLength, ArmLengthTo, DeltaTime, ArmRotationSpeed);
	PlayerUpperSpringArm->SetWorldLocation(FVector(SpringArm->GetComponentLocation().X,SpringArm->GetComponentLocation().Y,0));
	paperSprite->SetWorldLocation(FVector(SpringArm->GetComponentLocation().X, SpringArm->GetComponentLocation().Y, 900.0f));

}

void UPlayerCamera::CameraVector()
{
	FHitResult* HitResult = new FHitResult();
	float TraceRange = 10.0f;
	float TraceRadius = 5.0f;

	FVector StartTrace = this->GetComponentLocation();
	FVector ForwardVector = this->GetForwardVector();
	FVector EndTrace = ((ForwardVector*5000.0f) + StartTrace);
	FCollisionQueryParams* TraceParams = new FCollisionQueryParams();

	if (GetWorld()->LineTraceSingleByChannel(*HitResult, StartTrace, EndTrace, ECC_Visibility, *TraceParams)) {
		DrawDebugLine(GetWorld(), StartTrace, EndTrace, FColor(255, 0, 0), true);
	}
}

void UPlayerCamera::ShakeCameraActor()
{
	auto PlayerController = GetWorld()->GetFirstPlayerController();
	PlayerController->PlayerCameraManager->PlayCameraShake(cameraShake, 1.0f);
}
