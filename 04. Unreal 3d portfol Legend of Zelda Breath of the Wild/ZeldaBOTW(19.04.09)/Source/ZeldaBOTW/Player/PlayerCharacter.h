// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ZeldaBOTW.h"
#include "Player/PlayerMesh.h"
#include "Player/PlayerCamera.h"
#include "Player/PlayerUIWidget.h"
#include "Player/PlayerInventory.h"
#include "GameFramework/Character.h"
#include "PlayerCharacter.generated.h"

UCLASS()
class ZELDABOTW_API APlayerCharacter : public ACharacter
{
	GENERATED_BODY()
private:
	UPlayerMesh* PlayerMeshComponent;
	UPlayerCamera* PlayerCamera; 
	TSubclassOf<class UPlayerUIWidget> HDWidgetClass;
	UPlayerUIWidget* PlayerUI;
	UPROPERTY(Category = PlayerCharacter, EditAnywhere, BlueprintReadWrite, meta = (AllowPrivateAccess = "true"))
	UPlayerInventory* PlayerInventory;
protected:
	virtual void BeginPlay() override;
public:
	UFUNCTION()
	void getWeapon(UPrimitiveComponent* OverlappedComponent, AActor* OtherActor, UPrimitiveComponent* OtherComp, int32 OtherBodyIndex, bool bFromSweep, const FHitResult & SweepResult);
	APlayerCharacter();
	UPlayerMesh* getPlayerMeshComponent() { return PlayerMeshComponent; }
	UPlayerCamera* getPlayerCamera() { return PlayerCamera; }
	UPlayerUIWidget* getPlayerUI() { return PlayerUI; }
	UPlayerInventory* getPlayerInvenroty() { return PlayerInventory; }
};
