// Fill out your copyright notice in the Description page of Project Settings.

#include "PlayerInventory.h"
#include "Player/PlayerCharacter.h"
#include "Player/Controller/PlayerCharacterController.h"
#include "Player/PlayerUIWidget.h"

UPlayerInventory::UPlayerInventory()
{
	PrimaryComponentTick.bCanEverTick = false;
	PlayerMoney = 10;
}

void UPlayerInventory::AddSwordInventory(AWeapon_Equip * sword)
{
	Weapon_SwordList.Add(sword);
}

void UPlayerInventory::AddBowdInventory(AWeapon_Equip * Bow)
{
	Weapon_BowList.Add(Bow);
}

void UPlayerInventory::AddShieldInventory(AWeapon_Equip * Shield)
{
	Weapon_ShieldList.Add(Shield);
}

void UPlayerInventory::AddFoodInventory(AItemClassFood * Food)
{
	Item_FoodList.Add(Food);
}

void UPlayerInventory::AddMaterialInventory(AItemClassMaterial * Material)
{
	Item_MaterialList.Add(Material);
}

void UPlayerInventory::AddPantsInventory(AItemClassClothes * Pants)
{
	Item_PantsList.Add(Pants);
}

void UPlayerInventory::AddShirtsInventory(AItemClassClothes * Shirts)
{
	Item_ShirtsList.Add(Shirts);
}

void UPlayerInventory::DeleteWaepon_SwordList(AWeapon_Equip * weapon)
{
	Weapon_SwordList.Remove(weapon);
}

void UPlayerInventory::DeleteWaepon_BowList(AWeapon_Equip * weapon)
{
	Weapon_BowList.Remove(weapon);
}

void UPlayerInventory::DeleteWaepon_ShieldList(AWeapon_Equip * weapon)
{
	Weapon_ShieldList.Remove(weapon);
}

void UPlayerInventory::DeleteWaepon_ShirtsList(AItemClassClothes * weapon)
{
	Item_ShirtsList.Remove(weapon);
}

void UPlayerInventory::DeleteWaepon_PantsList(AItemClassClothes * weapon)
{
	Item_PantsList.Remove(weapon);
}

