// Fill out your copyright notice in the Description page of Project Settings.

#include "ItemClassMaterial.h"

AItemClassMaterial::AItemClassMaterial() {
	MaterialMesh = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("MaterialMesh"));

	RootComponent = MaterialMesh;
	MaterialMesh->SetSimulatePhysics(false);
	MaterialMesh->SetEnableGravity(true);

	MaterialMesh->SetCollisionProfileName(TEXT("Weapon"));
	MaterialMesh->ComponentTags.Add(FName("Weapon"));
	this->Tags.Add(FName("Material"));
}


