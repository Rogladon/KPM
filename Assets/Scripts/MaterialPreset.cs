using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialPreset : MonoBehaviour
{
	public Material originalMaterial;
    [ContextMenu("Create")]
	public void CreatePreset() {
		if (originalMaterial == null) {
			SkinnedMeshRenderer skinnedMeshRender;
			MeshRenderer meshRenderer;
			if (TryGetComponent(out skinnedMeshRender)) {
				originalMaterial = skinnedMeshRender.material;
				skinnedMeshRender.material = Instantiate(originalMaterial);
			}
			if (TryGetComponent(out meshRenderer)) {
				originalMaterial = meshRenderer.material;
				meshRenderer.material = Instantiate(originalMaterial);
			}
		}
	}
}
