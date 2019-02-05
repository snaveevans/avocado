import { IconDefinition } from "@fortawesome/fontawesome-svg-core";

export class AppRoute {
  constructor(
    public text: string,
    public icon: IconDefinition,
    public path: string
  ) {}
}
