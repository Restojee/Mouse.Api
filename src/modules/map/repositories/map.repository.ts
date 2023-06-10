import {
  Inject,
  Injectable
} from '@nestjs/common';
import { MapEntity } from "../../../database/entities/map.entity";
import { Repository } from "sequelize-typescript";
import {
  MapCreateArgs,
  MapGetArgs,
  MapUpdateArgs
} from "../map.types";

@Injectable()
export class MapRepository {
  constructor(@Inject('MAPS_REPOSITORY') private repository: Repository<MapEntity>) {}

  async getMap(mapGetArgs: MapGetArgs): Promise<MapEntity> {
    const { mapId } = mapGetArgs;
    return await this.repository.findByPk(mapId);
  }

  async getMaps(): Promise<MapEntity[]> {
    return await this.repository.findAll();
  }

  async createMap(mapCreateArgs: MapCreateArgs): Promise<MapEntity> {
    const { name, description, userId } = mapCreateArgs;
    const result = await this.repository.create({ name, description, userId });
    return await this.repository.findByPk(result.id);
  }

  async updateMap(mapUpdateArgs: MapUpdateArgs): Promise<MapEntity> {
    const { id, image, name, description } = mapUpdateArgs;
    await this.repository.update({ id, image, name, description }, { where: { id } });
    return this.repository.findByPk(mapUpdateArgs.id);
  }
}
