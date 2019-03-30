// Fill out your copyright notice in the Description page of Project Settings.

#include "ItemClasClothes.h"

AItemClasClothes::AItemClasClothes()
{
	ClothesMesh = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("ClothesMesh"));

	RootComponent = ClothesMesh;
	ClothesMesh->SetSimulatePhysics(false);
	ClothesMesh->SetEnableGravity(true);

	ClothesMesh->SetCollisionProfileName(TEXT("Weapon"));
	ClothesMesh->ComponentTags.Add(FName("Weapon"));
	this->Tags.Add(FName("Clothes"));
}


