// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ZeldaBOTW.h"
#include "Object/ItemClass.h"
#include "ItemClassClothes.generated.h"

/**
 * 
 */
UCLASS()
class ZELDABOTW_API AItemClassClothes : public AItemClass
{
	GENERATED_BODY()
	
public:
	AItemClassClothes();
	UPROPERTY(VisibleAnywhere, Category = Weapon)
		USkeletalMesh* ClothesMesh;

	void setClothesMesh(USkeletalMesh* mesh);
	USkeletalMesh* getClothesMesh() { return ClothesMesh; }
	
};
