export const manifests: Array<UmbExtensionManifest> = [
  {
    name: "Automate Click Up Entrypoint",
    alias: "Automate.ClickUp.Entrypoint",
    type: "backofficeEntryPoint",
    js: () => import("./entrypoint.js"),
  },
];
