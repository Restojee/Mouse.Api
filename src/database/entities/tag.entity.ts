import { MapTagEntity } from './map-tag.entity';
import {
  HasMany,
  Column,
  Table,
  ForeignKey,
  Model
} from "sequelize-typescript";
import { UserEntity } from "./user.entity";
import {
  InferAttributes,
  InferCreationAttributes
} from "sequelize";

@Table({ modelName: "tags" })
export class TagEntity extends Model<InferAttributes<TagEntity>, InferCreationAttributes<TagEntity>> {
  @Column
  name: string;

  @Column
  description: string;

  @Column
  @ForeignKey(() => UserEntity)
  userId: number;

  @Column
  public createdAt: Date = new Date();

  @Column
  public updatedAt: Date = new Date();

  @ForeignKey(() => UserEntity)
  user: UserEntity;

  @HasMany(() => MapTagEntity)
  mapTags: MapTagEntity[]
}
