// Fill out your copyright notice in the Description page of Project Settings.

#include "BrokenParts.h"

ABrokenParts::ABrokenParts()
{
 	PrimaryActorTick.bCanEverTick = true;
	DestructibleComponent = CreateDefaultSubobject<UDestructibleComponent>(TEXT("Destructible Component"));
	RootComponent = DestructibleComponent;
	DestructibleComponent->SetSimulatePhysics(true);

	DestructibleComponent->SetCollisionProfileName(TEXT("BrokenObject"));
}

void ABrokenParts::setDestructibleComponentMesh(UDestructibleMesh * mesh)
{
	DestructibleComponent->SetDestructibleMesh(mesh);
}

void ABrokenParts::TakeDamage()
{
	ABLOG(Warning, TEXT("Damage!broken"));
	DestructibleComponent->ApplyDamage(10.0f,GetActorLocation(),FVector(0,0,0),10.0f);
}

