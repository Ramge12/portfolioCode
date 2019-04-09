// Fill out your copyright notice in the Description page of Project Settings.

#include "PlayerCharacterShowUI.h"
#include "PaperSpriteComponent.h"
#include "PaperSprite.h"
#include "Player/PlayerMesh.h"
#include "Runtime/CoreUObject/Public/UObject/ConstructorHelpers.h"
#include "Runtime/Engine/Classes/Engine/TextureRenderTarget2D.h"

#include "Player/Controller/PlayerCharacterController.h"
#include "Player/PlayerMesh.h"
#include "Player/PlayerCharacter.h"
#include "Player/PlayerWeapon.h"
#include "Runtime/Engine/Classes/Components/StaticMeshComponent.h"

APlayerCharacterShowUI::APlayerCharacterShowUI()
{
 	PrimaryActorTick.bCanEverTick = true;

	PlayerUICamera = CreateDefaultSubobject<UCameraComponent>(TEXT("PlayerUICamera"));
	SpringArm = CreateDefaultSubobject<USpringArmComponent>(TEXT("SPRINGARM"));
	playerCaputre = CreateDefaultSubobject<USceneCaptureComponent2D>(TEXT("playerCaputre"));

	faceMesh = CreateDefaultSubobject<USkeletalMeshComponent>(TEXT("FaceMesh"));
	hairMesh = CreateDefaultSubobject<USkeletalMeshComponent>(TEXT("HairMesh"));
	upperMesh = CreateDefaultSubobject<USkeletalMeshComponent>(TEXT("UpperMesh"));
	underMesh = CreateDefaultSubobject<USkeletalMeshComponent>(TEXT("UnderMesh"));

	Weapon_Sword = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("Weapon_Sword"));
	Weapon_Shield = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("Weapon_Shield"));
	Weapon_Bow = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("Weapon_Bow"));
	ArrowBag = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("ArrowBag"));

	SetSkeletalMeshComponent(faceMesh, TEXT("/Game/PlayerCharacter/Mesh/PlayerMesh/Face/PlayerCharacter.PlayerCharacter"));
	SetSkeletalMeshComponent(hairMesh, TEXT("/Game/PlayerCharacter/Mesh/PlayerMesh/Hair/1/Hair.Hair"));
	SetSkeletalMeshComponent(upperMesh, TEXT("/Game/PlayerCharacter/Mesh/PlayerMesh/Pants/1/Pants.Pants"));
	SetSkeletalMeshComponent(underMesh, TEXT("/Game/PlayerCharacter/Mesh/PlayerMesh/Shirts/1/shirts2.shirts2"));
	RootComponent = faceMesh;
	hairMesh->SetupAttachment(faceMesh);
	upperMesh->SetupAttachment(faceMesh);
	underMesh->SetupAttachment(faceMesh);

	SpringArm->SetupAttachment(RootComponent);
	PlayerUICamera->SetupAttachment(SpringArm);

	SpringArm->TargetArmLength = 75.0f;
	SpringArm->SetRelativeRotation(FRotator(-10.0f, 90.0f, 0.0f));

	SetAnimClass(TEXT("/Game/PlayerCharacter/Animation/BP_Animation/PlayerUI.PlayerUI_C"));

	playerCaputre->SetupAttachment(PlayerUICamera);
	SetMeshPosition(0, 0, -42.0f);
	ConstructorHelpers::FObjectFinder<UTextureRenderTarget2D> m_Texture(TEXT("/Game/UI/PlayerUI/PlayerUI.PlayerUI"));
	if (m_Texture.Succeeded()) {
		playerCaputre->TextureTarget = m_Texture.Object;
	}

}

void APlayerCharacterShowUI::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);
	auto playerController = GetWorld()->GetFirstPlayerController();
	auto pcClass = Cast< APlayerCharacterController>(playerController);
	auto PlayerCharacter = pcClass->getPlayerCharacter();
	auto playerMesh = PlayerCharacter->getPlayerMeshComponent();
	if (IsValid(playerMesh)) {
		faceMesh->SetSkeletalMesh(playerMesh->getFaceMesh()->SkeletalMesh);
		hairMesh->SetSkeletalMesh(playerMesh->getHairMesh()->SkeletalMesh);
		upperMesh->SetSkeletalMesh(playerMesh->getUpperMesh()->SkeletalMesh);
		underMesh->SetSkeletalMesh(playerMesh->getUnderMesh()->SkeletalMesh);
	}
	SetWeaponUI();
}

void APlayerCharacterShowUI::SetSkeletalMeshComponent(USkeletalMeshComponent * skeletalMesh, const TCHAR * Channel)
{
	ConstructorHelpers::FObjectFinder<USkeletalMesh> m_Skeletal(Channel);
	if (m_Skeletal.Succeeded()) {
		skeletalMesh->SetSkeletalMesh(m_Skeletal.Object);
	}
}

void APlayerCharacterShowUI::SetAnimClass(const TCHAR * AnimChannel)
{
	faceMesh->SetAnimationMode(EAnimationMode::AnimationBlueprint);
	static ConstructorHelpers::FClassFinder<UAnimInstance> Player_ANIM(AnimChannel);
	if (Player_ANIM.Succeeded()) {
		faceMesh->SetAnimInstanceClass(Player_ANIM.Class);
		hairMesh->SetAnimInstanceClass(Player_ANIM.Class);
		upperMesh->SetAnimInstanceClass(Player_ANIM.Class);
		underMesh->SetAnimInstanceClass(Player_ANIM.Class);
	}
}

void APlayerCharacterShowUI::SetMeshPosition(float X, float Y, float Z)
{
	faceMesh->SetRelativeLocation(FVector(X, Y, Z));
}

void APlayerCharacterShowUI::SetWeaponUI()
{
	auto playerController = GetWorld()->GetFirstPlayerController();
	auto pcClass = Cast< APlayerCharacterController>(playerController);
	auto PlayerCharacter = pcClass->getPlayerCharacter();
	auto PlayerSword = pcClass->getPlayerWeapon()->getPlayerSworld();
	auto PlayerShield = pcClass->getPlayerWeapon()->getPlayerShield();
	auto PlayerBow = pcClass->getPlayerWeapon()->getPlayerBow();
	auto PlayerArrowBag = pcClass->getPlayerWeapon()->getPlayerArrowBag();
	SetWeaponPlayerUI(Weapon_Sword, PlayerSword);
	SetWeaponPlayerUI(Weapon_Shield, PlayerShield);
	SetWeaponPlayerUI(Weapon_Bow, PlayerBow);
	SetWeaponPlayerUI(ArrowBag, PlayerArrowBag);
}

void APlayerCharacterShowUI::SetWeaponPlayerUI(UStaticMeshComponent * weapon, AWeapon_Equip * playerWeapon)
{
	if (IsValid(playerWeapon)) {
		weapon->SetStaticMesh(playerWeapon->getWeaponMesh()->GetStaticMesh());
		weapon->AttachToComponent(faceMesh, FAttachmentTransformRules::KeepWorldTransform, playerWeapon->getWeaponMesh()->GetAttachSocketName());
		weapon->SetRelativeLocation(playerWeapon->getWeaponMesh()->GetRelativeTransform().GetLocation());
		weapon->SetRelativeRotation(playerWeapon->getWeaponMesh()->GetRelativeTransform().GetRotation());
		weapon->SetRelativeScale3D(playerWeapon->getWeaponMesh()->GetComponentScale());
		weapon->SetVisibility(playerWeapon->getWeaponMesh()->IsVisible());
	}
}
