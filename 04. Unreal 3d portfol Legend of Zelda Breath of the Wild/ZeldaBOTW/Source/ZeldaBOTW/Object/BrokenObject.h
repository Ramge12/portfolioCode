// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ZeldaBOTW.h"
#include "Object/ItemClass.h"
#include "Player/PlayerCharacterState.h"
#include "GameFramework/Actor.h"
#include "BrokenObject.generated.h"

UCLASS()
class ZELDABOTW_API ABrokenObject : public AActor
{
	GENERATED_BODY()
private:
	FTimerHandle BrokenHandler;
	bool carryOn;
public:
	UPROPERTY(Category = Item, EditAnywhere, BlueprintReadOnly, meta = (AllowPrivateAccess = "true"))
		FName ItemName;
	UPROPERTY(Category = Item, EditAnywhere, BlueprintReadOnly, meta = (AllowPrivateAccess = "true"))
		UTexture2D*  ItemImage;
	UPROPERTY(Category = Item, EditAnywhere, BlueprintReadOnly, meta = (AllowPrivateAccess = "true"))
		FString ItemInformation;
	UPROPERTY(Category = Item, EditAnywhere, BlueprintReadOnly, meta = (AllowPrivateAccess = "true"))
		ItemTypeValue ItemType;
	UPROPERTY(Category = Item, EditAnywhere, BlueprintReadOnly, meta = (AllowPrivateAccess = "true"))
		float ItemPrice;


	UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = Object)
		TSubclassOf<class UDestructibleComponent> brokenParts;
	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Object)
		class UDestructibleMesh* DestructibleMesh;
	UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = Object)
		UDestructibleComponent* broken;

	UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = Object)
		TSubclassOf<class ABrokenParts> brokenPartsClass;

	UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = Object)
		UStaticMeshComponent* StaticComponent;
	UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = Object)
		bool IsDestroyed;
	UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = Object)
		float MaxHealth;
	UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = Object)
		float CurrentHealth;
	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Object)
		E_PlayerState ExtraPlayerState;

	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Object)
		TSubclassOf<class AActor>  DropItemClass;
	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Object)
		AActor*  DropItem;
	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Object)
		UStaticMesh*  DropItemMesh;
public:	
	// Sets default values for this actor's properties
	ABrokenObject();
	UFUNCTION()
		void OnHitComponent(UPrimitiveComponent* HitComponent, AActor* OtherActor, UPrimitiveComponent* OtherComp, FVector NormalImpulse, const FHitResult& Hit);

protected:
	// Called when the game starts or when spawned
	virtual void BeginPlay() override;

public:	
	void Broken();
	void setCharacterEquip(USkeletalMeshComponent* MeshComponent, const TCHAR * Socket);

	void setObjectHPDamage();
	void setBrokenColiision(bool value);
	void setExtraState(E_PlayerState value) { ExtraPlayerState = value; }
	void setCarryon(bool value) { carryOn = value; }


	bool getObjectCarry() { return carryOn; }
	E_PlayerState getPlayerExtraState() { return ExtraPlayerState; }
	UStaticMeshComponent* getObjectMesh() { return StaticComponent; }
};
