// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ZeldaBOTW.h"
#include "Engine/SkeletalMesh.h"
#include "PlayerMesh.generated.h"

UCLASS()
class ZELDABOTW_API UPlayerMesh : public USkeletalMesh
{
	GENERATED_BODY()
public:
	UPlayerMesh();

	USkeletalMeshComponent* rootMesh;
	USkeletalMeshComponent* faceMesh;
	USkeletalMeshComponent* hairMesh;
	USkeletalMeshComponent* upperMesh;
	USkeletalMeshComponent* underMesh;

public:
	void SetSkeletalMeshComponent(USkeletalMeshComponent* skeletalMesh, const TCHAR* Channel);
	void SetRootSkeletalMeshComponent(USkeletalMeshComponent* skeletalMesh);
	void SetAnimClass(const TCHAR * AnimChannel);
	void SetMeshPosition(float X, float Y, float Z);

	USkeletalMeshComponent* getRootMesh() { return faceMesh; }
	USkeletalMeshComponent* getFaceMesh() { return faceMesh; }
	USkeletalMeshComponent* getHairMesh() { return hairMesh; }
	USkeletalMeshComponent* getUpperMesh() { return upperMesh; }
	USkeletalMeshComponent* getUnderMesh() { return underMesh; }

};
