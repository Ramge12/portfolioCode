// Fill out your copyright notice in the Description page of Project Settings.

#include "BrokenObject.h"
#include "Weapon/Weapon_Equip.h"
#include "Weapon/Weapon_Arrow.h"
#include "Object/BrokenParts.h"

// Sets default values
ABrokenObject::ABrokenObject()
{
 	PrimaryActorTick.bCanEverTick = false;
	
	RootComponent = CreateDefaultSubobject<USceneComponent>(TEXT("Scene Component"));
	StaticComponent = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("StaticComponent"));
	DropItemMesh = CreateDefaultSubobject<UStaticMesh>(TEXT("DropItemMesh"));
	DropItem = CreateDefaultSubobject<AActor>(TEXT("DropItem"));

	RootComponent = StaticComponent;
	StaticComponent->SetNotifyRigidBodyCollision(true);
	IsDestroyed = false;
	carryOn = false;

	MaxHealth = 3.f;
	StaticComponent->SetCollisionProfileName(TEXT("BrokenObject"));
	StaticComponent->ComponentTags.Add(FName("ExtraObject"));
	DropItemClass = AActor::StaticClass();
	brokenParts = UDestructibleComponent::StaticClass();
	brokenPartsClass = ABrokenParts::StaticClass();
}

void ABrokenObject::OnHitComponent(UPrimitiveComponent * HitComponent, AActor * OtherActor, UPrimitiveComponent * OtherComp, FVector NormalImpulse, const FHitResult & Hit)
{
	if (!IsDestroyed && OtherComp->ComponentHasTag("Weapon")) {
		setObjectHPDamage();
	}
}

void ABrokenObject::BeginPlay()
{
	Super::BeginPlay();

	StaticComponent->OnComponentHit.AddDynamic(this, &ABrokenObject::OnHitComponent);
	CurrentHealth = MaxHealth;
}

void ABrokenObject::Broken()
{
	auto parts = GetWorld()->SpawnActor<ABrokenParts>(brokenPartsClass, GetActorLocation() ,StaticComponent->GetRelativeTransform().Rotator());
	if (IsValid(parts)) {
		parts->setDestructibleComponentMesh(DestructibleMesh);
		parts->TakeDamage();
	}
	StaticComponent->SetSimulatePhysics(false);
	StaticComponent->SetCollisionProfileName(TEXT("NoCollision"));
	StaticComponent->SetVisibility(false);
	IsDestroyed = true;

	AActor* Item = GetWorld()->SpawnActor<AActor>(DropItemClass, StaticComponent->GetRelativeTransform().GetLocation(), FRotator::ZeroRotator);
	auto ItemClass = Cast< AWeapon_Equip>(Item);
	if (ItemClass != nullptr) {
		ItemClass->getWeaponMesh()->SetStaticMesh(DropItemMesh);

		ItemClass->setItemName(ItemName);
		ItemClass->setItemImage(ItemImage);
		ItemClass->setItemInformation(ItemInformation);
		ItemClass->setItmeTypeValue(ItemType);
		ItemClass->setItemPrice(ItemPrice);
	}
	//this->Destroy();
}

void ABrokenObject::setCharacterEquip(USkeletalMeshComponent * MeshComponent, const TCHAR * Socket)
{
	if (IsValid(StaticComponent)) {
		StaticComponent->SetSimulatePhysics(false);
		StaticComponent->AttachToComponent(MeshComponent, FAttachmentTransformRules::SnapToTargetNotIncludingScale, Socket);
	}
}

void ABrokenObject::setObjectHPDamage()
{
	CurrentHealth--;
	if (CurrentHealth < 1) {
		Broken();
	}
}

void ABrokenObject::setBrokenColiision(bool value)
{
	if (value) {
		StaticComponent->SetCollisionProfileName(TEXT("BrokenObject"));
		carryOn = false;
	}
	else {
		StaticComponent->SetCollisionProfileName(TEXT("Broken"));
		carryOn = true;
	}
}

