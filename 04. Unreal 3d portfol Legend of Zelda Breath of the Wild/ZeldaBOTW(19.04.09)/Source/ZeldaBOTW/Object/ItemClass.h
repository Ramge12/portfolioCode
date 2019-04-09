// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ZeldaBOTW.h"
#include "Components/Image.h"
#include "GameFramework/Actor.h"
#include "ItemClass.generated.h"


UENUM()
enum class ItemTypeValue : uint8 {
	ItemType_Weapon		UMETA(DisplayName = "ItemType_Weapon"),
	ItemType_Bow		UMETA(DisplayName = "ItemType_Bow"),
	ItemType_Material	UMETA(DisplayName = "ItemType_Material"),
	ItemType_Shield		UMETA(DisplayName = "ItemType_Shield"),
	ItemType_Tshirts	UMETA(DisplayName = "ItemType_Tshirts"),
	ItemType_Pants		UMETA(DisplayName = "ItemType_Pants")
};

UCLASS()
class ZELDABOTW_API AItemClass : public AActor
{
	GENERATED_BODY()
private:
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

public:	
	AItemClass();

	FName getItemName() { return ItemName; }
	UTexture2D*  getItemImage() { return ItemImage; }
	FString getItemInformation() { return ItemInformation; }
	ItemTypeValue getItemTypeValue() { return ItemType; }
	float getItemPrice() { return ItemPrice; }

	void setItemName(FName Name) { ItemName= Name; }
	void setItemImage(UTexture2D*  Image) { ItemImage= Image; }
	void setItemInformation(FString Information) { ItemInformation= Information; }
	void setItemTypeValue(ItemTypeValue Type) { ItemType= Type; }
	void setItemPrice(float Price) { ItemPrice= Price; }
	
};
