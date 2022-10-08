import { AppRoutes } from "../routes/AppRoutes";
import { MenuIcons } from "./MenuIcons.Enum";

export type MenuItem = {
    text: string;
    route: AppRoutes;
    icon: MenuIcons;
}