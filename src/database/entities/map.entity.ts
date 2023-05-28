import { UserEntity } from './user.entity';
import { CommentEntity } from './comment.entity';
import { MapTagEntity } from './map-tag.entity';
import {
  BelongsTo,
  Column,
  ForeignKey,
  HasMany,
  Model,
  Table,
} from "sequelize-typescript";
import {
  InferAttributes,
  InferCreationAttributes
} from "sequelize";

@Table({ modelName: "maps" })
export class MapEntity extends Model<InferAttributes<MapEntity>, InferCreationAttributes<MapEntity>> {
  @Column
  name: string;

  @Column
  description: string;

  @Column
  image: string;

  @Column
  @ForeignKey(() => UserEntity)
  userId: number;

  @Column
  public createdAt: Date = new Date();

  @Column
  public updatedAt: Date = new Date();

  @BelongsTo(() => UserEntity)
  user: UserEntity;

  @HasMany(() => CommentEntity)
  comments: Array<CommentEntity>;

  @HasMany(() => MapTagEntity)
  mapTags: Array<MapTagEntity>;

  @HasMany(() => MapTagEntity)
  note: Array<MapTagEntity>
}