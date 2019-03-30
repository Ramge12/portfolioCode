// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ZeldaBOTW.h"
#include "Camera/CameraComponent.h"
#include "PlayerCamera.generated.h"

UENUM()
enum class PlayerViewState : uint8 {
	View_IDLE		UMETA(DisplayName = "View_IDLE"),
	View_SHOT		UMETA(DisplayName = "View_SHOT"),
	View_SHIELD		UMETA(DisplayName = "View_SHIELD")
};

UCLASS()
class ZELDABOTW_API UPlayerCamera : public UCameraComponent
{
	GENERATED_BODY()
public:
	class USpringArmComponent* SpringArm;
	class USpringArmComponent* PlayerUpperSpringArm;
	class UPaperSpriteComponent* paperSprite;
	class UTextureRenderTarget2D* RenderTarget;
	class USceneCaptureComponent2D* playerCaputre;
	TSubclassOf<class UPlayerCameraShake> cameraShake;
	TSubclassOf<class UTextureRenderTarget2D> RenderTargetClass;

private:
	float ArmLengthTo = 0.0f;
	float UpperArmLengthTo = 0.0f;
	float ArmRotationSpeed = 0.0f;
	PlayerViewState ViewState;
public:
	UPlayerCamera();
	void setSpringArmCameraInRoot(UCapsuleComponent* root);
	void ChangeView(PlayerViewState view);
	virtual void TickComponent(float DeltaTime, ELevelTick TickType, FActorComponentTickFunction* ThisTickFunction) override;

	void CameraVector();
	void ShakeCameraActor();

	PlayerViewState getPlayerViewState() { return ViewState; }
};