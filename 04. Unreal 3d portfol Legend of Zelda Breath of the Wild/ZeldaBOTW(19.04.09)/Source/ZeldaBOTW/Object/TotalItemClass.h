// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ZeldaBOTW.h"
#include "Object/ItemClass.h"
#include "Components/Image.h"
#include "Components/ActorComponent.h"
#include "TotalItemClass.generated.h"

USTRUCT(BlueprintType)
struct FItemClassStruct
{
	GENERATED_USTRUCT_BODY();
public:
	UPROPERTY(Category = TotalItem, EditAnywhere, BlueprintReadWrite, meta = (AllowPrivateAccess = "true"))
	UStaticMesh* ItemMesh;
	UPROPERTY(Category = TotalItem, EditAnywhere, BlueprintReadWrite, meta = (AllowPrivateAccess = "true"))
	FName ItemName;
	UPROPERTY(Category = TotalItem, EditAnywhere, BlueprintReadWrite, meta = (AllowPrivateAccess = "true"))
	UTexture2D*  ItemImage;
	UPROPERTY(Category = TotalItem, EditAnywhere, BlueprintReadWrite, meta = (AllowPrivateAccess = "true"))
	FString ItemInformation;
	UPROPERTY(Category = TotalItem, EditAnywhere, BlueprintReadWrite, meta = (AllowPrivateAccess = "true"))
	ItemTypeValue ItemType;
	UPROPERTY(Category = TotalItem, EditAnywhere, BlueprintReadWrite, meta = (AllowPrivateAccess = "true"))
	float ItemPrice;
	UPROPERTY(Category = TotalItem, EditAnywhere, BlueprintReadWrite, meta = (AllowPrivateAccess = "true"))
	USkeletalMesh* itemSkeletalMeshComponent;
};

UCLASS( ClassGroup=(Custom), meta=(BlueprintSpawnableComponent) )
class ZELDABOTW_API UTotalItemClass : public UActorComponent
{
	GENERATED_BODY()
private:
	UPROPERTY(Category = TotalItem, EditAnywhere, BlueprintReadWrite, meta = (AllowPrivateAccess = "true"))
		TArray < FItemClassStruct> ItemClassStruct;



public:	
	// Sets default values for this component's properties
	UTotalItemClass();

protected:
	// Called when the game starts
	virtual void BeginPlay() override;

public:	
	// Called every frame
	virtual void TickComponent(float DeltaTime, ELevelTick TickType, FActorComponentTickFunction* ThisTickFunction) override;

	TArray < FItemClassStruct> getStructItem() { return ItemClassStruct; }
	
};
