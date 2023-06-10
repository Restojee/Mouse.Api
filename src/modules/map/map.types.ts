export type CreateFavoriteArgs = {
    mapId: number;
    userId: number
}

export type GetFavoriteMapByUserAndMapArgs = {
    mapId: number;
    userId: number
}

export type AddToFavoritesMapAsyncArgs = {
    mapId: number;
    userId: number;
}

//

export type GetCompletedByUserAndMap = {
    userId: number;
    mapId: number;
}

export type CreateCompletedArgs = {
    mapId: number;
    userId: number;
    image: string;
}

export type UpdateCompletedArgs = {
    image: string
    completedId: number;
}

export type CompleteMapByUserAsyncArgs = {
    mapId: number;
    userId: number;
    image: string;
}

//

export type UpdateMapImageAsyncArgs = {
    mapId: number,
    image: string
}

export type MapUpdateArgs = {
    id: number;
    name?: string;
    description?: string;
    image?: string;
}

export type MapCreateArgs = {
    name: string;
    description?: string;
    userId: number;
}

export type MapGetArgs = {
    mapId: number
}

import { Map } from "./models/map";

export type MapField = keyof Pick<Map,
  | "id"
  | "image"
  | "description"
  | "createdAt"
  | "updatedAt"
  >;