// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ZeldaBOTW.h"
#include "Object/ItemClass.h"
#include "ItemClasClothes.generated.h"

/**
 * 
 */
UCLASS()
class ZELDABOTW_API AItemClasClothes : public AItemClass
{
	GENERATED_BODY()
public:
	AItemClasClothes();
	UPROPERTY(VisibleAnywhere, Category = Weapon)
		UStaticMeshComponent* ClothesMesh;

	
	
	
};
