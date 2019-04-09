// Fill out your copyright notice in the Description page of Project Settings.

#include "Weapon_Arrow.h"
#include "DrawDebugHelpers.h"
#include "Enemy/EnemyCharacter.h"
#include "Enemy/EnemyCharacterState.h"
#include "Enemy/BossCharacter.h"
#include "Player/PlayerCharacter.h"
#include "Player/Controller/PlayerCharacterController.h"
#include "Player/PlayerCharacterState.h"

AWeapon_Arrow::AWeapon_Arrow()
{
 	PrimaryActorTick.bCanEverTick = true;
	ArrowMesh = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("ArrowMesh"));

	static ConstructorHelpers::FObjectFinder<UStaticMesh>SK_ARROW(TEXT("/Game/OBJECT/Bow/Arrow/WeaponArrow.WeaponArrow"));
	if (SK_ARROW.Succeeded()) {
		ArrowMesh->SetStaticMesh(SK_ARROW.Object);
	}
	RootComponent = ArrowMesh;
	ArrowMesh->SetEnableGravity(false);
	ArrowMesh->SetSimulatePhysics(true);
	ArrowMesh->SetNotifyRigidBodyCollision(true);
	ArrowMesh->ComponentTags.Add(FName("Weapon"));
	ArrowMesh->SetCollisionProfileName(TEXT("Weapon"));
	this->Tags.Add(FName("Weapon"));
	touchActor = false;
	gravityBullte = 200.0f;
	Velocity = FVector::ZeroVector;
	CrushVec = FVector::ZeroVector;
}

void AWeapon_Arrow::BeginPlay()
{
	Super::BeginPlay();
	ArrowMesh->OnComponentHit.AddDynamic(this, &AWeapon_Arrow::hitComponent);
}

void AWeapon_Arrow::hitComponent(UPrimitiveComponent * HitComponent, AActor * OtherActor, UPrimitiveComponent * OtherComp, FVector NormalImpulse, const FHitResult & Hit)
{
	ArrowMesh->SetSimulatePhysics(false);
	ArrowMesh->AttachToComponent(Hit.GetComponent(), FAttachmentTransformRules::KeepWorldTransform);
}

void AWeapon_Arrow::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);

	FVector StartTrace = this->GetActorLocation();
	FVector EndTrace = (Velocity*DeltaTime) + StartTrace;
	if (Velocity != FVector::ZeroVector) {
		SetActorRelativeRotation(FRotator((Velocity*DeltaTime).Rotation().Roll, (Velocity*DeltaTime).Rotation().Yaw - 90.0f, -(Velocity*DeltaTime).Rotation().Pitch));
	}
	FHitResult HitResult;
	FCollisionQueryParams CollisionParams;
	CollisionParams.AddIgnoredActor(this);

	if (GetWorld()->LineTraceSingleByChannel(HitResult, StartTrace, EndTrace, ECC_Destructible, CollisionParams)) {
		if (HitResult.GetActor()) {
			if (hitEnemy) {
				if (!HitResult.GetComponent()->ComponentHasTag("PlayerCharacter") && !HitResult.GetComponent()->ComponentHasTag("Weapon")) {
					touchActor = true;
					Velocity = FVector::ZeroVector;
					gravityBullte = 0.0f;
					EndTrace = this->GetActorLocation();
					CrushVec = this->GetActorLocation();
					//DrawDebugSolidBox(GetWorld(), HitResult.ImpactPoint, FVector(3.0f), FColor::Blue, true);

					AEnemyCharacter* EnemyCharacter = Cast< AEnemyCharacter>(HitResult.GetActor());
					ABossCharacter* BossCahracter = Cast< ABossCharacter>(HitResult.GetActor());
					if (EnemyCharacter != nullptr) {
						EnemyCharacter->getEnemyState()->setOnBattle(true);
						EnemyCharacter->Hurt();
						RootComponent->SetVisibility(false);
						this->Destroy();
					}
					else if (BossCahracter != nullptr) {
						BossCahracter->HurtBoss();
						RootComponent->SetVisibility(false);
						this->Destroy();
					}
					else {
						hitEnemy = false;
					}
				}
			}
			else if (hitPlayer) {
				if (!HitResult.GetComponent()->ComponentHasTag("Enemy") && !HitResult.GetComponent()->ComponentHasTag("Weapon")) {
					touchActor = true;
					Velocity = FVector::ZeroVector;
					gravityBullte = 0.0f;
					EndTrace = this->GetActorLocation();
					CrushVec = this->GetActorLocation();
					//DrawDebugSolidBox(GetWorld(), HitResult.ImpactPoint, FVector(3.0f), FColor::Blue, true);

					auto PlayerController = GetWorld()->GetFirstPlayerController();
					APlayerCharacter* PlayerChracter = Cast< APlayerCharacter>(HitResult.GetActor());
					APlayerCharacterController* playerController = Cast< APlayerCharacterController>(PlayerController);
					if (IsValid(PlayerChracter)&&IsValid(playerController)) {
						playerController->getBattleController()->PlayerHurt();
						RootComponent->SetVisibility(false);
						this->Destroy();
					}
					else {
						hitPlayer = false;
					}
				}
			}
		}
	}

	if (touchActor) {
		ArrowMesh->GetBodyInstance()->bLockXTranslation = true;
		ArrowMesh->GetBodyInstance()->bLockYTranslation = true;
		ArrowMesh->GetBodyInstance()->bLockZTranslation = true;
		ArrowMesh->SetSimulatePhysics(false);
		ArrowMesh->SetCollisionProfileName(TEXT("NoCollision"));

		auto PlayerController = GetWorld()->GetFirstPlayerController();
		APlayerCharacterController* PController = Cast< APlayerCharacterController>(PlayerController);
		APlayerCharacter* playerCharacter = PController->getPlayerCharacter();
		float dist = FVector::Dist(GetActorLocation(), playerCharacter->GetActorLocation());
		if (dist < 150.0f) {
			if(!hitPlayer && !hitEnemy)PController->getPlayerWeapon()->AddArrow();
			this->Destroy();
		}
	}
	else{
		//DrawDebugLine(GetWorld(), StartTrace, EndTrace, FColor::Green, true);
		SetActorLocation(EndTrace);
		Velocity += FVector(0.0f, 0.0f, -gravityBullte)*DeltaTime;
	}
}

