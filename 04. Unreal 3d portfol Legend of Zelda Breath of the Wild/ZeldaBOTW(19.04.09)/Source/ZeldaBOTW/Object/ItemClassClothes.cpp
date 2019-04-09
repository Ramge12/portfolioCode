// Fill out your copyright notice in the Description page of Project Settings.

#include "ItemClassClothes.h"

AItemClassClothes::AItemClassClothes()
{
	ClothesMesh = CreateDefaultSubobject<USkeletalMesh>(TEXT("ClothesMesh"));
}

void AItemClassClothes::setClothesMesh(USkeletalMesh * mesh)
{
	ClothesMesh=(mesh);
}
