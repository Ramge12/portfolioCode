// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "Object/ItemClass.h"
#include "ItemClassFood.generated.h"

/**
 * 
 */
UCLASS()
class ZELDABOTW_API AItemClassFood : public AItemClass
{
	GENERATED_BODY()
	
public:
	AItemClassFood();
	UPROPERTY(VisibleAnywhere, Category = Weapon)
		UStaticMeshComponent* FoodMesh;
	
	
};
