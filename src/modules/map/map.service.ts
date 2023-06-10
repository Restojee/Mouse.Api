import {
  BadRequestException,
  Injectable,
  NotFoundException
} from '@nestjs/common';
import { MapRepository } from './repositories/map.repository';
import { Map } from './models/map';
import { MapUpdateRequest } from './models/map-update-request';
import { MapsGetRequest } from './models/maps-get-request';
import {
  plainToClass,
  plainToInstance
} from 'class-transformer';
import {CompletedRepository} from "./repositories/completed.repository";
import {
  AddToFavoritesMapAsyncArgs,
  CompleteMapByUserAsyncArgs,
  MapCreateArgs,
  UpdateMapImageAsyncArgs
} from "./map.types";
import { FavoriteRepository } from "./repositories/favorite.repository";

@Injectable()
export class MapService {
  constructor(
      private mapRepository: MapRepository,
      private favoriteRepository: FavoriteRepository,
      private completedRepository: CompletedRepository
  ) {}

  async getMap(mapId: number): Promise<Map> {
    const result = await this.mapRepository.getMap({ mapId });
    if (result) {
      return plainToClass(Map, result);
    }
    throw new NotFoundException(`Карта с идентификатором ${ mapId } не найдена`);
  }

  async getMaps(mapGetRequest: MapsGetRequest): Promise<Array<Map>> {
    const result = await this.mapRepository.getMaps();
    return plainToInstance(Map, result);
  }

  async createMap(mapCreateRequest: MapCreateArgs): Promise<Map> {
    const result = await this.mapRepository.createMap(mapCreateRequest);
    return plainToClass(Map, result);
  }

  async updateMap(mapUpdateRequest: MapUpdateRequest): Promise<Map> {
    const foundMap = await this.getMap(mapUpdateRequest.id);
    if (!foundMap) {
      throw new BadRequestException(`Карта с идентификатором ${ mapUpdateRequest.id } не найдена`);
    }
    const result = await this.mapRepository.updateMap(mapUpdateRequest);
    return plainToClass(Map, result);
  }

  async uploadMapImage(updateMapImageAsync: UpdateMapImageAsyncArgs): Promise<Map> {
    const foundMap = await this.getMap(updateMapImageAsync.mapId);

    if (!foundMap) {
      throw new BadRequestException(`Карта с идентификатором ${ updateMapImageAsync.mapId } не найдена`);
    }

    const { mapId, image } = updateMapImageAsync;

    const result = await this.mapRepository.updateMap({ id: mapId, image });

    return plainToClass(Map, result);
  }

  async completeMap(completeMapByUserAsyncArgs: CompleteMapByUserAsyncArgs): Promise<Map> {
    const { mapId, userId, image } = completeMapByUserAsyncArgs;

    const foundMap = await this.mapRepository.getMap({ mapId });

    if (!foundMap) {
      throw new NotFoundException(`Карта с идентификатором ${ mapId } не найдена`);
    }

    const foundCompleted = await this.completedRepository.getCompletedByUserAndMap({ userId, mapId });

    if (foundCompleted) {
      await this.completedRepository.updateCompleted({ image, completedId: foundCompleted.id })
    }

    else {
      await this.completedRepository.createCompleted({ image, mapId, userId })
    }

    const result = await this.mapRepository.getMap({ mapId });

    return plainToClass(Map, result);
  }

  async favoriteMap(addToFavoritesMapAsyncArgs: AddToFavoritesMapAsyncArgs): Promise<Map> {
    const { mapId, userId } = addToFavoritesMapAsyncArgs;

    const foundMap = await this.mapRepository.getMap({ mapId });

    if (!foundMap) {
      throw new NotFoundException(`Карта с идентификатором ${ mapId } не найдена`);
    }

    await this.favoriteRepository.createFavorite({ mapId, userId });

    const result = await this.mapRepository.getMap({ mapId });

    return plainToClass(Map, result);
  }

  async unFavoriteMap(addToFavoritesMapAsyncArgs: AddToFavoritesMapAsyncArgs): Promise<Map> {
    const { mapId, userId } = addToFavoritesMapAsyncArgs;

    const favoriteFound = await this.favoriteRepository.getFavoriteByUserAndMap({ mapId, userId });

    if (!favoriteFound) {
      throw new NotFoundException(`Карта с идентификатором ${ mapId } не добавлена в избранное`);
    }

    await this.favoriteRepository.removeFavorite(favoriteFound.id);

    const result = await this.mapRepository.getMap({ mapId });

    return plainToClass(Map, result);
  }
}
