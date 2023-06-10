import {
    Inject,
    Injectable
} from "@nestjs/common";
import { Repository } from "sequelize-typescript";
import { FavoriteEntity } from "../../../database/entities/favorite.entity";
import {
    CreateFavoriteArgs,
    GetFavoriteMapByUserAndMapArgs
} from "../map.types";

@Injectable()
export class FavoriteRepository {
    constructor(
        @Inject('FAVORITE_REPOSITORY')
        private repository: Repository<FavoriteEntity>
    ) {}
    async removeFavorite(favoriteId: number): Promise<void> {
        await this.repository.destroy({ where: { id: favoriteId } })
    }

    async createFavorite(createOneFavoriteRequest: CreateFavoriteArgs): Promise<FavoriteEntity> {
        const { userId, mapId } = createOneFavoriteRequest;
        return await this.repository.create({ userId, mapId });
    }

    async getFavoriteByUserAndMap(getFavoriteMapByUserAndMapArgs: GetFavoriteMapByUserAndMapArgs) {
        const { userId, mapId } = getFavoriteMapByUserAndMapArgs;
        return await this.repository.findOne({ where: { userId, mapId } })
    }
}
