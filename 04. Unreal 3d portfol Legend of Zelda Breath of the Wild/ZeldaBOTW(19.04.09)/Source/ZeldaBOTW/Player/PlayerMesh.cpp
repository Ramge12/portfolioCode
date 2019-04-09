// Fill out your copyright notice in the Description page of Project Settings.

#include "PlayerMesh.h"

UPlayerMesh::UPlayerMesh()
{
	faceMesh = CreateDefaultSubobject<USkeletalMeshComponent>(TEXT("FaceMesh"));
	hairMesh = CreateDefaultSubobject<USkeletalMeshComponent>(TEXT("HairMesh"));
	upperMesh = CreateDefaultSubobject<USkeletalMeshComponent>(TEXT("UpperMesh"));
	underMesh = CreateDefaultSubobject<USkeletalMeshComponent>(TEXT("UnderMesh"));
	rootMesh = CreateDefaultSubobject<USkeletalMeshComponent>(TEXT("RootMesh"));

	SetSkeletalMeshComponent(faceMesh, TEXT("/Game/PlayerCharacter/Mesh/PlayerMesh/Face/PlayerCharacter.PlayerCharacter"));
	SetSkeletalMeshComponent(hairMesh,	TEXT("/Game/PlayerCharacter/Mesh/PlayerMesh/Hair/1/Hair.Hair"));
	SetSkeletalMeshComponent(underMesh, TEXT("/Game/PlayerCharacter/Mesh/PlayerMesh/Pants/1/Pants.Pants"));
	SetSkeletalMeshComponent(upperMesh, TEXT("/Game/PlayerCharacter/Mesh/PlayerMesh/Shirts/1/shirts2.shirts2"));

	BaseupperMesh = upperMesh->SkeletalMesh;
	BaseunderMesh = underMesh->SkeletalMesh;

	faceMesh->SetupAttachment(rootMesh);
	hairMesh->SetupAttachment(rootMesh);
	upperMesh->SetupAttachment(rootMesh);
	underMesh->SetupAttachment(rootMesh);
}

void UPlayerMesh::SetSkeletalMeshComponent(USkeletalMeshComponent * skeletalMesh, const TCHAR * Channel)
{
	ConstructorHelpers::FObjectFinder<USkeletalMesh> m_Skeletal(Channel);
	if (m_Skeletal.Succeeded()) {
		skeletalMesh->SetSkeletalMesh(m_Skeletal.Object);
	}
}

void UPlayerMesh::SetRootSkeletalMeshComponent(USkeletalMeshComponent * skeletalMesh)
{
	rootMesh->SetupAttachment(skeletalMesh);
}

void UPlayerMesh::SetAnimClass(const TCHAR * AnimChannel)
{
	static ConstructorHelpers::FClassFinder<UAnimInstance> Player_ANIM(AnimChannel);
	if (Player_ANIM.Succeeded()) {
		faceMesh->SetAnimInstanceClass(Player_ANIM.Class);
		hairMesh->SetAnimInstanceClass(Player_ANIM.Class);
		upperMesh->SetAnimInstanceClass(Player_ANIM.Class);
		underMesh->SetAnimInstanceClass(Player_ANIM.Class);
	}
}


void UPlayerMesh::SetMeshPosition(float X, float Y, float Z)
{
	rootMesh->SetRelativeLocation(FVector(X, Y, Z));
}

void UPlayerMesh::SetUpperMesh(USkeletalMesh * skeletalMesh)
{
	upperMesh->SetSkeletalMesh(skeletalMesh);
}

void UPlayerMesh::SetUnderMesh(USkeletalMesh * skeletalMesh)
{
	underMesh->SetSkeletalMesh(skeletalMesh);
}

void UPlayerMesh::SetBaseUpperMesh()
{
	upperMesh->SetSkeletalMesh(BaseupperMesh);
}

void UPlayerMesh::SetBaseUnderMesh()
{
	underMesh->SetSkeletalMesh(BaseunderMesh);
}
