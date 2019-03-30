// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ZeldaBOTW.h"
#include "GameFramework/Actor.h"
#include "Weapon_Arrow.generated.h"

UCLASS()
class ZELDABOTW_API AWeapon_Arrow : public AActor
{
	GENERATED_BODY()

private:
	bool hitEnemy;
	bool hitPlayer;
	bool touchActor;
	float gravityBullte;
	FVector CrushVec;
	FTimerHandle ArrowTimeHandler;

public:
	UPROPERTY(VisibleAnywhere, Category = Weapon)
	UStaticMeshComponent* ArrowMesh;
	UPROPERTY(EditAnywhere, Category = Weapon)
	FVector Velocity = FVector::ZeroVector;
public:	
	AWeapon_Arrow();

protected:
	virtual void BeginPlay() override;

public:	
	UFUNCTION()
	void hitComponent(UPrimitiveComponent* HitComponent, AActor* OtherActor, UPrimitiveComponent* OtherComp, FVector NormalImpulse, const FHitResult& Hit);
	virtual void Tick(float DeltaTime) override;

	void setVelocity(FVector VelocityValue) { Velocity = VelocityValue; }
	void setHitEnemy(bool value) { hitEnemy = value; }
	void setHitPlayer(bool value) { hitPlayer = value; }
	UStaticMeshComponent* getMesh() { return ArrowMesh; }
};
