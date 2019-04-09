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

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Mesh, Meta = (AllowPrivateAccess = true))
	USkeletalMeshComponent* upperMesh;
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Mesh, Meta = (AllowPrivateAccess = true))
	USkeletalMeshComponent* underMesh;

	USkeletalMesh* BaseupperMesh;
	USkeletalMesh* BaseunderMesh;

public:
	void SetSkeletalMeshComponent(USkeletalMeshComponent* skeletalMesh, const TCHAR* Channel);
	void SetRootSkeletalMeshComponent(USkeletalMeshComponent* skeletalMesh);
	void SetAnimClass(const TCHAR * AnimChannel);
	void SetMeshPosition(float X, float Y, float Z);
	void SetUpperMesh(USkeletalMesh* skeletalMesh);
	void SetUnderMesh(USkeletalMesh* skeletalMesh);
	void SetBaseUpperMesh();
	void SetBaseUnderMesh();

	USkeletalMesh* getBaseupperMesh(){ return BaseupperMesh;}
	USkeletalMesh* getBaseunderMesh(){ return BaseunderMesh;}

	USkeletalMeshComponent* getRootMesh() { return faceMesh; }
	USkeletalMeshComponent* getFaceMesh() { return faceMesh; }
	USkeletalMeshComponent* getHairMesh() { return hairMesh; }
	USkeletalMeshComponent* getUpperMesh() { return upperMesh; }
	USkeletalMeshComponent* getUnderMesh() { return underMesh; }

};
