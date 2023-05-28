import { MapEntity } from './map.entity';
import { CommentEntity } from './comment.entity';
import {
  Column,
  Table,
  HasMany, Model
} from "sequelize-typescript";
import {
  InferAttributes,
  InferCreationAttributes
} from "sequelize";

@Table({ modelName: "users" })
export class UserEntity extends Model<InferAttributes<UserEntity>, InferCreationAttributes<UserEntity>> {
  @Column
  username: string;

  @Column
  password: string;

  @Column
  public createdAt: Date = new Date();

  @Column
  public updatedAt: Date = new Date();

  @HasMany(() => MapEntity)
  maps: MapEntity[]

  @HasMany(() => CommentEntity)
  comments: CommentEntity[]
}
