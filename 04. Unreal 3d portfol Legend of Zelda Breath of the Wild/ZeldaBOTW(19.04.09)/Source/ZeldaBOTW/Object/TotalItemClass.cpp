// Fill out your copyright notice in the Description page of Project Settings.

#include "TotalItemClass.h"


// Sets default values for this component's properties
UTotalItemClass::UTotalItemClass()
{

	PrimaryComponentTick.bCanEverTick = true;


}


// Called when the game starts
void UTotalItemClass::BeginPlay()
{
	Super::BeginPlay();

	// ...
	
}


// Called every frame
void UTotalItemClass::TickComponent(float DeltaTime, ELevelTick TickType, FActorComponentTickFunction* ThisTickFunction)
{
	Super::TickComponent(DeltaTime, TickType, ThisTickFunction);

	// ...
}

