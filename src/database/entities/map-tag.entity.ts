import { TagEntity } from './tag.entity';
import { MapEntity } from './map.entity';
import {
  BelongsTo, Column,
  ForeignKey, Model,
  Table
} from "sequelize-typescript";
import {InferAttributes, InferCreationAttributes} from "sequelize";

@Table({ modelName: "map-tags" })
export class MapTagEntity extends Model<InferAttributes<MapTagEntity>, InferCreationAttributes<MapTagEntity>> {

  @Column
  @ForeignKey(() => MapEntity)
  mapId: number;

  @Column
  @ForeignKey(() => TagEntity)
  tagId: number;

  @Column
  public createdAt: Date = new Date();

  @Column
  public updatedAt: Date = new Date();

  @BelongsTo(() => MapEntity)
  map: MapEntity;

  @BelongsTo(() => TagEntity)
  tag: TagEntity;
}
