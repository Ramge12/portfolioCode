// Fill out your copyright notice in the Description page of Project Settings.

#include "Weapon_Equip.h"
#include "DrawDebugHelpers.h"
#include "Enemy/EnemyCharacterState.h"
#include "Enemy/EnemyCharacter.h"
#include "Enemy/BossCharacter.h"
#include "Player/PlayerCharacter.h"
#include "Player/PlayerCharacterState.h"
#include "Player/Controller/PlayerCharacterController.h"

AWeapon_Equip::AWeapon_Equip()
{
 	PrimaryActorTick.bCanEverTick = true;
	WeaponMesh = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("WeaponMesh"));

	hitEnemy = false;
	hitPlayer = false;
	CurWeaponType = WeaponType::WeaponType_Sword;

	RootComponent = WeaponMesh;
	WeaponMesh->SetSimulatePhysics(false);
	WeaponMesh->SetEnableGravity(true);

	WeaponMesh->SetCollisionProfileName(TEXT("Weapon"));
	WeaponMesh->ComponentTags.Add(FName("Weapon"));
	this->Tags.Add(FName("Weapon"));
}

void AWeapon_Equip::BeginPlay()
{
	Super::BeginPlay();
	WeaponMesh->OnComponentBeginOverlap.AddDynamic(this, &AWeapon_Equip::WeaponCollision);

}

void AWeapon_Equip::WeaponCollision(UPrimitiveComponent * OverlappedComponent, AActor * OtherActor, UPrimitiveComponent * OtherComp, int32 OtherBodyIndex, bool bFromSweep, const FHitResult & SweepResult)
{
	if (OtherComp->ComponentHasTag("Enemy") && hitEnemy) {
		AEnemyCharacter* EnemyCharacter = Cast< AEnemyCharacter>(OtherComp->GetOwner());
		if (IsValid(EnemyCharacter)) {
			EnemyCharacter->getEnemyState()->setOnBattle(true);
			EnemyCharacter->Hurt();
		}
		hitEnemy = false;
	}
	else if (OtherComp->ComponentHasTag("PlayerCharacter") && hitPlayer) {
		auto PlayerController = GetWorld()->GetFirstPlayerController();
		APlayerCharacterController* PController = Cast< APlayerCharacterController>(PlayerController);
		if (IsValid(PController)) {
			PController->getBattleController()->PlayerHurt();
			hitPlayer = false;
		}
	}
	else{
		FHitResult HitResult;
		FCollisionQueryParams CollisionParams;
		CollisionParams.AddIgnoredActor(this);
		bool bResult =
			GetWorld()->SweepSingleByChannel(
				HitResult,
				GetActorLocation() ,
				GetActorLocation() + GetActorUpVector()*80.f,
				FQuat::Identity,
				ECollisionChannel::ECC_GameTraceChannel1,
				FCollisionShape::MakeSphere(20.0f),
				CollisionParams
			);
		if (bResult) {
			ABossCharacter* BossCahracter = Cast< ABossCharacter>(HitResult.GetActor());
			if (BossCahracter != nullptr) {
				BossCahracter->HurtBoss();
			}
			hitEnemy = false;
		}
	}
}

void AWeapon_Equip::setCharacterEquip(USkeletalMeshComponent * MeshComponent, const TCHAR * Socket)
{
	if (IsValid(WeaponMesh)) {
		WeaponMesh->SetSimulatePhysics(false);
		WeaponMesh->SetCollisionProfileName(TEXT("Weapon"));
		WeaponMesh->AttachToComponent(MeshComponent, FAttachmentTransformRules::SnapToTargetNotIncludingScale, Socket);
	}
}

void AWeapon_Equip::setDropItemInformation(AItemClass* info)
{
	setItemName(info->getItmeName());
	setItemImage(info->getItemImage());
	setItemInformation(info->getItemInformation());
	setItmeTypeValue(info->getItmeTypeValue());
	setItemPrice(info->getItemPrice());
}

