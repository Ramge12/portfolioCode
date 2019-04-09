// Fill out your copyright notice in the Description page of Project Settings.

#include "PlayerCharacter.h"
#include "DrawDebugHelpers.h"
#include "Weapon/Weapon_Equip.h"
#include "Object/ItemClass.h"
#include "Components/WidgetComponent.h"
#include "Player/Controller/PlayerCharacterController.h"
#include "Player/PlayerWeapon.h"
#include "Object/ItemClassFood.h"
#include "Object/ItemClassMaterial.h"

APlayerCharacter::APlayerCharacter()
{
 	PrimaryActorTick.bCanEverTick = false;
	PlayerMeshComponent = CreateDefaultSubobject<UPlayerMesh>(TEXT("PlayerMeshComponent"));
	PlayerCamera = CreateDefaultSubobject<UPlayerCamera>(TEXT("PlayerCamera"));
	PlayerInventory = CreateDefaultSubobject<UPlayerInventory>(TEXT("PlayerInventory"));

	PlayerMeshComponent->SetRootSkeletalMeshComponent(GetMesh());
	PlayerCamera->setSpringArmCameraInRoot(GetCapsuleComponent());

	GetCapsuleComponent()->SetCapsuleRadius(10);
	GetCapsuleComponent()->SetCapsuleHalfHeight(30);
	GetMesh()->SetRelativeLocationAndRotation(FVector(0.0f, 0.0f, -32.0f), FRotator(0.0f, -90.0f, 0.0f));
	bUseControllerRotationYaw = false;

	GetCharacterMovement()->bOrientRotationToMovement = true;
	GetCharacterMovement()->RotationRate = FRotator(0.0f, 720.0f, 0.0f);
	GetCharacterMovement()->bUseControllerDesiredRotation = false;

	GetCharacterMovement()->JumpZVelocity = 400.0f;
	GetCharacterMovement()->GravityScale = 1.0f;

	GetMesh()->SetAnimationMode(EAnimationMode::AnimationBlueprint);
	PlayerMeshComponent->SetAnimClass(TEXT("/Game/PlayerCharacter/Animation/BP_Animation/PlayerBPAnimation.PlayerBPAnimation_C"));

	GetCharacterMovement()->GetNavAgentPropertiesRef().bCanCrouch = true;
	GetCharacterMovement()->CrouchedHalfHeight = 20.0f;

	GetCapsuleComponent()->SetCollisionProfileName(TEXT("PlayerCharacter"));
	GetCapsuleComponent()->ComponentTags.Add(FName("PlayerCharacter"));

	static ConstructorHelpers::FClassFinder<UPlayerUIWidget> UI_AIM(TEXT("/Game/UI/Player_UI.Player_UI_C"));
	if (UI_AIM.Succeeded()) {
		HDWidgetClass = UI_AIM.Class;
	}
}

void APlayerCharacter::BeginPlay()
{
	Super::BeginPlay();
	GetCapsuleComponent()->OnComponentBeginOverlap.AddDynamic(this, &APlayerCharacter::getWeapon);
	
	PlayerUI = CreateWidget<UPlayerUIWidget>(Cast< APlayerCharacterController>(GetWorld()->GetFirstPlayerController()), HDWidgetClass);
	auto PlayerChracterController = Cast< APlayerCharacterController>(GetWorld()->GetFirstPlayerController());
	PlayerUI->setPlayerCharacterController(PlayerChracterController);
	PlayerUI->AddToViewport();
	PlayerUI->SetPlayerAim(false);
	PlayerUI->SetHP(12, 12);
}

void APlayerCharacter::getWeapon(UPrimitiveComponent * OverlappedComponent, AActor * OtherActor, UPrimitiveComponent * OtherComp, int32 OtherBodyIndex, bool bFromSweep, const FHitResult & SweepResult)
{
	if (OtherComp->ComponentHasTag("Weapon") && OtherActor->GetAttachParentActor() == nullptr) {
		auto itemClass = Cast< AItemClass>(OtherActor);
		if (IsValid(itemClass)) {
			PlayerUI->PrintMessageBox("Item get");
		}

		AWeapon_Equip* EquapWeapon2 = Cast<AWeapon_Equip>(OtherActor);
		if (EquapWeapon2 != nullptr) {
			auto PlayerController = GetWorld()->GetFirstPlayerController();
			APlayerCharacterController* PController = Cast< APlayerCharacterController>(PlayerController);
			EquapWeapon2->getWeaponMesh()->SetSimulatePhysics(false);
			if (EquapWeapon2->getCurWeaponType() == WeaponType::WeaponType_Sword) {
				PController->getPlayerWeapon()->AddSwordList(EquapWeapon2);
			}
			else if (EquapWeapon2->getCurWeaponType() == WeaponType::WeaponType_Shield) {
				PController->getPlayerWeapon()->AddShieldList(EquapWeapon2);
			}
			else if (EquapWeapon2->getCurWeaponType() == WeaponType::WeaponType_Bow) {
				PController->getPlayerWeapon()->AddBowdList(EquapWeapon2);
			}
			else if (EquapWeapon2->getCurWeaponType() == WeaponType::WeaponType_ArrowBag) {
				PController->getPlayerWeapon()->AddArrowBagList(EquapWeapon2);
			}
			PController->getPlayerWeapon()->setWeaponComponent(E_Weapon::Weapon_None, PlayerMeshComponent->getRootMesh(), TEXT(""));
		}
		AItemClassFood* ItemClassFodd = Cast<AItemClassFood>(OtherActor);
		if (ItemClassFodd != nullptr) {
			OtherComp->SetVisibility(false);
			PlayerInventory->AddFoodInventory(ItemClassFodd);
		}
		AItemClassMaterial* ItemClassMaterial = Cast<AItemClassMaterial>(OtherActor);
		if (ItemClassMaterial != nullptr) {
			OtherComp->SetVisibility(false);
			PlayerInventory->AddMaterialInventory(ItemClassMaterial);
		}
	}
}

