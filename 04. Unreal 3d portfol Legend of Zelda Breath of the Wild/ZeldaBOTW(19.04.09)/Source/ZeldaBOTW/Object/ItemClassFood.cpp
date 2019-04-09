// Fill out your copyright notice in the Description page of Project Settings.

#include "ItemClassFood.h"

AItemClassFood::AItemClassFood()
{
	FoodMesh = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("WeaponMesh"));
	
	RootComponent = FoodMesh;
	FoodMesh->SetSimulatePhysics(false);
	FoodMesh->SetEnableGravity(true);

	FoodMesh->SetCollisionProfileName(TEXT("Weapon"));
	FoodMesh->ComponentTags.Add(FName("Weapon"));
	this->Tags.Add(FName("Food"));
}


