// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "Object/ItemClass.h"
#include "ItemClassMaterial.generated.h"

/**
 * 
 */
UCLASS()
class ZELDABOTW_API AItemClassMaterial : public AItemClass
{
	GENERATED_BODY()
public:
	AItemClassMaterial();
	UPROPERTY(VisibleAnywhere, Category = Weapon)
		UStaticMeshComponent* MaterialMesh;

	
	
	
};
